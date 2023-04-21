using System;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Office365.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.User.Commands
{
    public class SendEmailForgotPassCommandHandler : BaseDbContextWithMediatorHandler<SendEmailForgotPassCommand, bool>
    {
        private readonly IGraphProvider _microsoftGraphProvider;
        private readonly IdentityServerConfig _config;

        public SendEmailForgotPassCommandHandler(ApplicationDbContext context, IMediator mediator, 
            IGraphProvider microsoftGraphProvider, IOptions<IdentityServerConfig> config) : base(context, mediator)
        {
            _microsoftGraphProvider = microsoftGraphProvider;
            _config = config.Value;
        }

        public override async Task<bool> Handle(SendEmailForgotPassCommand request, CancellationToken cancellationToken)
        {
            bool status = false;

            try
            {
                if(request.User == null)
                    throw new Exception("User is Required");


                #region  SendEmail
                string path = $"wwwroot/EmailTemplate/ResetPassword.html";

                string content = System.IO.File.ReadAllText(path);
                content = content.Replace("{{name}}", request.User.DisplayName);
                content = content.Replace("{{callbackUrl}}", request.CallbackUrl);
                await _microsoftGraphProvider.SendEmail(request.User.Email, "โปรดรีเซ็ทรหัสผ่านของคุณ", content);
                #endregion
                
                status = true;
            }
            catch(Exception e)
            {
                throw e;
            }
            
            return await Task.FromResult(status);
        }
    }
}