using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Place.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Commands;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.Privilege.Commands
{
    public class InviteUserCommandHandler : BaseDbContextWithMediatorHandler<InviteUserCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;
        public InviteUserCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<string>> Handle(InviteUserCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถ Invite User ได้"
            };

            try
            {
                GetPlaceNameQuery placeNameQuery = new GetPlaceNameQuery()
                {
                    Id = request.PlaceId
                };
                var place = await _mediator.Send(placeNameQuery);
                if(place == null)
                    throw new Exception("ไม่พบข้อมูลสถานที่");

                List<string> inActive = new List<string>(); 
                List<ApplicationUser> users = new List<ApplicationUser>();
                foreach(var u in request.Users)
                {
                    var user = await _userManager.FindByIdAsync(u);
                    if(user == null)
                        throw new Exception("ไม่พบข้อมูลผู้ใช้งาน");

                    if(user.InActiveStatus || user.IsDeleted)
                    {
                        inActive.Add(u);
                        continue;
                    }

                    users.Add(user);
                }

                if(inActive.Any())
                {
                    throw new Exception($"ข้อมูลผู้ใช้งานถูกปิดใช้งาน {inActive.Join(", ")}");
                }
                
                foreach(var u in users)
                {
                    #region Insert Data
                    Domain.Privilege privilege = new Domain.Privilege()
                    {
                        PlaceId = request.PlaceId,
                        PrivilegeTypeCode = "PRIVILEGE_TYPE_PERSON",
                        UserId = u.Id,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = request.CreateUserId
                    };
                    _context.Add(privilege);
                    #endregion

                    #region Send Invite Email
                    SendEmailInviteUserCommand inviteUserCommand = new SendEmailInviteUserCommand()
                    {
                        PlaceName = $"{place.Name_TH} ({place.Name_EN})",
                        User = u
                    };
                    await _mediator.Send(inviteUserCommand);
                    #endregion
                }
                
                var res = await _context.SaveChangesAsync();

                cmdResult.Status = res > 0;
                if(cmdResult.Status)
                {
                    cmdResult.Data = "Invite User สำเร็จ";
                    cmdResult.Message = "Invite User สำเร็จ";
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