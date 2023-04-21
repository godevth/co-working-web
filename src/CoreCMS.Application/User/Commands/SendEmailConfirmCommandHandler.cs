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
    public class SendEmailConfirmCommandHandler : BaseDbContextWithMediatorHandler<SendEmailConfirmCommand, bool>
    {
        private readonly IGraphProvider _microsoftGraphProvider;
        private readonly IdentityServerConfig _config;

        public SendEmailConfirmCommandHandler(ApplicationDbContext context, IMediator mediator, 
            IGraphProvider microsoftGraphProvider, IOptions<IdentityServerConfig> config) : base(context, mediator)
        {
            _microsoftGraphProvider = microsoftGraphProvider;
            _config = config.Value;
        }

        public override async Task<bool> Handle(SendEmailConfirmCommand request, CancellationToken cancellationToken)
        {
            bool status = false;

            try
            {
                if(request.User == null)
                    throw new Exception("User is Required");

                if(string.IsNullOrEmpty(request.RandomCode))
                    throw new Exception("RandomCode is Required");

                #region  SendEmail
                string confirmEmailUrl = _config.Authority + "/ConfirmEmail";   
                var callbackUrl = string.Format("{0}?userId={1}&randomCode={2}", confirmEmailUrl, request.User.Id, request.RandomCode);
                string path = $"wwwroot/EmailTemplate/confirm.html";

                string content = System.IO.File.ReadAllText(path);
                content = content.Replace("{{name}}", request.User.DisplayName);
                content = content.Replace("{{callbackUrl}}", callbackUrl);
                await _microsoftGraphProvider.SendEmail(request.User.Email, "ยืนยันการลงทะเบียน", content);
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