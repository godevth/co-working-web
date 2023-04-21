using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CoreCMS.Application.Privilege.Commands
{
    public class ImportInviteCommandHandler : BaseDbContextWithMediatorHandler<ImportInviteCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;
        public ImportInviteCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<string>> Handle(ImportInviteCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;

            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารบันทึกข้อมูลได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) 
            {
                try
                {
                    List<string>  userList = new List<string>();
                    foreach(var data in request.ListUser)
                    {

                        try {
                            var addr = new System.Net.Mail.MailAddress(data.Email);
                        }
                        catch {
                            throw new Exception("Format Email '"+data.Email+"' นี้ไม่ถูกต้อง ไม่สามารถบันทึกข้อมูลได้");;
                        }

                        var user = await _userManager.FindByEmailAsync(data.Email);
                        
                        if(user != null)
                        {
                            userList.Add(user.Id);
                        }
                        else if(user == null)
                        {
                            throw new Exception("ไม่พบ Email '"+data.Email+"' นี้ในระบบ ไม่สามารถบันทึกข้อมูลได้");
                        }

                        var invite = _context.Privilege.Where(o => o.PlaceId ==  Guid.Parse(request.PlaceId)  && o.UserId == user.Id);

                        if(invite.Any())
                        {
                            throw new Exception(" Email '"+data.Email+"' นี้มีการส่ง Invite ไปแล้ว");
                        }
                    }

                    InviteUserCommand inviteUser = new InviteUserCommand()
                    {
                        PlaceId = Guid.Parse(request.PlaceId),
                        Users = userList
                    };
                    var var = await _mediator.Send(inviteUser);

                    cmdResult.Status = var.Status;
                    cmdResult.Message = "บันทึกข้อมูลสำเร็จ";
                    trx.Commit();

                }
                catch(Exception e)
                {
                    trx.Rollback();
                    cmdResult.Message = e.Message;
                }
            }

            return await Task.FromResult(cmdResult);
        }
    }
}