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
    public class ChangePasswordCommandHandler : BaseDbContextWithMediatorHandler<ChangePasswordCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;
        public ChangePasswordCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;

            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถเปลี่ยนรหัสผ่านได้"
            };

            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                    throw new Exception($"ไม่พบผู้ใช้งาน {request.UserId}");

                if (user.InActiveStatus || user.IsDeleted)
                    throw new Exception($"ผู้ใช้งาน {user.Email} ถูกปิดใช้งานไปแล้ว");

                if(user.OpenID)
                    throw new Exception("เปลี่ยนรหัสผ่านรองรับแค่สมาชิกทั่วไปเท่านั้น ผู้ใช้งาน OpenId กรุณาแก้ที่ระบบ MyClub");

                if(user.NormalizedUserName != user.NormalizedEmail)
                    throw new Exception("เปลี่ยนรหัสผ่านรองรับแค่สมาชิกทั่วไปเท่านั้น ผู้ใช้งาน Social Login กรุณาแก้ที่ระบบนั้นๆ");

                IdentityResult result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                if(!result.Succeeded)
                    throw new Exception($"{string.Join(", ", result.Errors.Select(o => o.Description).ToArray())}");
                    
                cmdResult.Data = user.Email;
                cmdResult.Status = true;
                cmdResult.Message = "เปลี่ยนรหัสผ่านสำเร็จ";
            }
            catch (Exception e) {
                cmdResult.Message = e.Message;
            }
            return await Task.FromResult(cmdResult);
        }
    }
}