using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CoreCMS.Application.User.Commands
{
    public class ConfirmEmailCommandHandler : BaseDbContextWithMediatorHandler<ConfirmEmailCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;
        public ConfirmEmailCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;

            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถยืนยันอีเมลได้"
            };
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if(user == null)
                    throw new Exception($"ไม่พบผู้ใช้งาน {request.UserId}");

                if(request.RandomCode.Trim() == user.RandomCode)
                {
                    if(user.ConfirmEmailDate.HasValue)
                        throw new Exception("คุณได้ยืนยันอีเมลไปแล้ว");

                    user.ConfirmEmailDate = DateTime.Now;
                    user.EmailConfirmed = true;
                    var result = await _userManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {
                        var sendEmailConfirmed = new SendEmailConfirmedCommand()
                        {
                            User = user
                        };
                        await _mediator.Send(sendEmailConfirmed);

                        cmdResult.Status = true;
                        cmdResult.Data = user.Email;
                        cmdResult.Message = "ยืนยันอีเมลสำเร็จ";
                    }
                    else
                    {
                        throw new Exception($"{string.Join(", ", result.Errors.Select(o => o.Description).ToArray())}");
                    }
                }
                
            }
            catch(Exception e)
            {
                 cmdResult.Message = e.Message;
            }
            
            return await Task.FromResult(cmdResult);
        }
    }
}