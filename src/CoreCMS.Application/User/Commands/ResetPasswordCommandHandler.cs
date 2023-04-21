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
    public class ResetPasswordCommandHandler : BaseDbContextWithMediatorHandler<ResetPasswordCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;

        public ResetPasswordCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;

            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถรีเซ็ทรหัสผ่านได้"
            };
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if(user == null)
                    throw new Exception($"ไม่พบผู้ใช้งาน {request.UserId}");

                if(user.NormalizedUserName != user.NormalizedEmail)
                    throw new Exception("รีเซ็ทรหัสผ่านรองรับแค่สมาชิกทั่วไปเท่านั้น ผู้ใช้งาน Social Login กรุณาแก้ที่ระบบนั้นๆ");

                var result = await _userManager.ResetPasswordAsync(user, request.Token.Trim(), request.Password.Trim());
                if(result.Succeeded)
                {
                    cmdResult.Status = true;
                    cmdResult.Data = user.Email;
                    cmdResult.Message = "รีเซ็ทรหัสผ่านสำเร็จ";
                }
                else
                {
                    throw new Exception($"{string.Join(", ", result.Errors.Select(o => o.Description).ToArray())}");
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