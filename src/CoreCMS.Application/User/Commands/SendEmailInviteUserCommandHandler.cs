using System;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Office365.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.User.Commands
{
    public class SendEmailInviteUserCommandHandler : BaseDbContextWithMediatorHandler<SendEmailInviteUserCommand, bool>
    {
        private readonly IGraphProvider _microsoftGraphProvider;
        private readonly CowConfig _config;

        public SendEmailInviteUserCommandHandler(ApplicationDbContext context, IMediator mediator, 
            IGraphProvider microsoftGraphProvider, IOptions<CowConfig> config) : base(context, mediator)
        {
            _microsoftGraphProvider = microsoftGraphProvider;
            _config = config.Value;
        }

        public override async Task<bool> Handle(SendEmailInviteUserCommand request, CancellationToken cancellationToken)
        {
            bool status = false;

            try
            {
                if(request.User == null)
                    throw new Exception("User is Required");
                
                if(string.IsNullOrEmpty(request.PlaceName))
                    throw new Exception("PlaceName is Required");

                #region  SendEmail
                string path = $"wwwroot/EmailTemplate/InviteUser.html";

                string content = System.IO.File.ReadAllText(path);
                content = content.Replace("{{name}}", request.User.DisplayName);
                content = content.Replace("{{email}}", request.User.Email);
                content = content.Replace("{{place}}", request.PlaceName);
                content = content.Replace("{{googlePlay}}", _config.GooglePlayUrl);
                content = content.Replace("{{appleStore}}", _config.AppleStoreUrl);
                await _microsoftGraphProvider.SendEmail(request.User.Email, "ได้รับการเชิญเรียบร้อยแล้ว", content);
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