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
    public class SendEmailConfirmedCommandHandler : BaseDbContextWithMediatorHandler<SendEmailConfirmedCommand, bool>
    {
        private readonly IGraphProvider _microsoftGraphProvider;
        private readonly CowConfig _config;

        public SendEmailConfirmedCommandHandler(ApplicationDbContext context, IMediator mediator, 
            IGraphProvider microsoftGraphProvider, IOptions<CowConfig> config) : base(context, mediator)
        {
            _microsoftGraphProvider = microsoftGraphProvider;
            _config = config.Value;
        }

        public override async Task<bool> Handle(SendEmailConfirmedCommand request, CancellationToken cancellationToken)
        {
            bool status = false;

            try
            {
                if(request.User == null)
                    throw new Exception("User is Required");

                #region  SendEmail
                string path = $"wwwroot/EmailTemplate/registered.html";

                string content = System.IO.File.ReadAllText(path);
                content = content.Replace("{{name}}", request.User.DisplayName);
                content = content.Replace("{{email}}", request.User.Email);
                content = content.Replace("{{googlePlay}}", _config.GooglePlayUrl);
                content = content.Replace("{{appleStore}}", _config.AppleStoreUrl);
                await _microsoftGraphProvider.SendEmail(request.User.Email, "ลงทะเบียนเรียบร้อยแล้ว", content);
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