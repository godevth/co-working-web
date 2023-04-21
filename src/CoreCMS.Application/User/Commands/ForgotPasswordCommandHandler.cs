using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Office365.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.User.Commands
{
    public class ForgotPasswordCommandHandler : BaseDbContextWithMediatorHandler<ForgotPasswordCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IGraphProvider _microsoftGraphProvider;
        private readonly IdentityServerConfig _config;

        public ForgotPasswordCommandHandler(ApplicationDbContext context, IMediator mediator, 
            UserManager<ApplicationUser> userManager, IGraphProvider microsoftGraphProvider, IOptions<IdentityServerConfig> config) : base(context, mediator)
        {
            _userManager = userManager;
            _microsoftGraphProvider = microsoftGraphProvider;
            _config = config.Value;
        }

        public override async Task<CommandResult<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;

            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถลืมรหัสผ่านได้"
            };
            try {
                var user = await _userManager.FindByEmailAsync(request.Email.Trim());

                if (user == null)
                    throw new Exception($"ไม่พบผู้ใช้งาน {request.Email}");

                if(user.OpenID)
                    throw new Exception("ลืมรหัสผ่านรองรับแค่สมาชิกทั่วไปเท่านั้น ผู้ใช้งาน OpenId กรุณาแก้ที่ระบบ MyClub");
                
                if(user.NormalizedUserName != user.NormalizedEmail)
                    throw new Exception("ลืมรหัสผ่านรองรับแค่สมาชิกทั่วไปเท่านั้น ผู้ใช้งาน Social Login กรุณาแก้ที่ระบบนั้นๆ");

                GenResetPasswordTokenCommand command = new GenResetPasswordTokenCommand()
                {
                    UserId = user.Id
                };
                var token = await _mediator.Send(command);
                if(token != null)
                {
                    #region  SendEmail
                    string forgotPasswordUrl = _config.Authority + "/ResetPassword";
                    var tokenEncode = WebUtility.UrlEncode(token);
                    var callbackUrl = string.Format("{0}?userId={1}&token={2}&", forgotPasswordUrl, user.Id, tokenEncode);

                    SendEmailForgotPassCommand sendEmailConfirm = new SendEmailForgotPassCommand()
                    {
                        User = user,
                        CallbackUrl = callbackUrl
                    };
                    bool sendEmail = await _mediator.Send(sendEmailConfirm);
                    if (!sendEmail)
                        throw new Exception("ไม่สามารถส่งอีเมลรีเซ็ทรหัสผ่านได้");
                    //string content = $"Dear {user.DisplayName} <br>";
                    //content += "<br>";              
                    //content += $"กรุณา Reset Password ด้วยการคลิกที่ <a href='{callbackUrl}'>link</a> <br> ";
                    //content += "ถ้าไม่ต้องการทำรายการดังกล่าวก็ไม่ต้องสนใจอีเมลฉบับนี้ <br> ";
                    //content += $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a> <br> If you didn't make this request, ignore this email. <br>";
                    //content += "<br>";
                    //content += "Thank You. <br>";
                    //content += "This is an auto-generated email <br>";
                    //await _microsoftGraphProvider.SendEmail(user.Email, "[Co-Working] Reset Password", content);
                    #endregion

                    cmdResult.Status = true;
                    cmdResult.Data = $"ส่งอีเมลไปที่ {request.Email} สำเร็จ";
                    cmdResult.Message = "ลืมรหัสผ่านสำเร็จ";
                }
            }
            catch(Exception e)
            {
                cmdResult.Message = e.Message;
                if(e.InnerException != null)
                    cmdResult.Message += e.InnerException.Message;
            }
            return await Task.FromResult(cmdResult);
        }
    }
}