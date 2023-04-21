using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Notification.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.Shared.Models;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace CoreCMS.Application.Notification.Commands
{
    public class MessageCanceledCommandHandler : IRequestHandler<MessageCanceledCommand, CommandResult<List<string>>>
    {
        private readonly ApiConfig _config;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public MessageCanceledCommandHandler(IOptions<ApiConfig> config, IMediator mediator, ApplicationDbContext context)
        {
            _config = config.Value;
            _mediator = mediator;
            _context = context;
        }

        public async Task<CommandResult<List<string>>> Handle(MessageCanceledCommand request, CancellationToken cancellationToken)
        {
            CommandResult<List<string>> cmdResult = new CommandResult<List<string>>()
            {
                Message  = "ไม่สามารถเรียก API Message Canceled ได้"
            };
            Response<List<string>> response = null;

             var unixTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //Request Token From SSO
            await RequestTokenSSO();

            var listjob = _context.LogNotification.Where(o => o.Notification.NotiCode == request.RefCode&&o.SetTimeSend>unixTimestamp && o.Notification.NotiTypeCode == request.NotificationType).Select(o => o.ResponeJobId).Distinct().ToArray();

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var url = _config.MessageCanceledUrl;
            RestClient cli = new RestClient(url);

            var method = Method.PUT;
            RestRequest req = new RestRequest("", method);

            //Header
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + _config.AccessTokenForNotification);

            //Body
            req.RequestFormat = DataFormat.Json;
            req.AddJsonBody(new[]
            {
                new 
                {
                    AppId = request.AppId,
                    MessageIDList = listjob
                }
            });

            var result = cli.Execute(req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(result.Content))
                {
                    response = JToken.Parse(result.Content).ToObject<Response<List<string>>>();
                    if(response != null && response.HttpStatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        cmdResult.Message = response.Message;
                        cmdResult.Data = response.Result;
                        cmdResult.Status = false;
                    }
                    else 
                    {
                        var update = _context.LogNotification.Where(o => o.Notification.NotiCode == request.RefCode && o.Notification.NotiTypeCode == request.NotificationType).ToList();
                        
                        if (update.Count>0) {
                            foreach (var up in update) {
                                up.InActiveStatus = true;
                                _context.Update(up);
                            }
                        }
                        _context.SaveChanges();
                        cmdResult.Message = "เรียก API Message Canceled สำเร็จ";
                        cmdResult.Data = response.Result;
                        cmdResult.Status = true;
                    }
                }

            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await RequestTokenSSO();
                await _mediator.Send(request);                
            }
            else 
            {
                if (!string.IsNullOrEmpty(result.Content))
                {
                    throw new Exception(result.Content.ToString());
                }
            }
            return await Task.FromResult(cmdResult);
        }

        private async Task<CommandResult<RequestTokenResponse>> RequestTokenSSO()
        {
            RequestTokenCommand requestTokenCommand = new RequestTokenCommand()
            {
                ClientId = _config.ClientId,
                ClientSecret = _config.Secret,
                GrantType = _config.GrantType,
                Scope = _config.Scope,
                AuthoritySSO = _config.AuthoritySSO,
                Authority = _config.Authority,
                AccessTokenForNotification = true
            };
            try
            {
                var res = await _mediator.Send(requestTokenCommand);
                if (res.Status && res.Data != null)
                {
                    _config.AccessTokenForNotification = res.Data.AccessToken;
                }
                return await Task.FromResult(res);
            }
            catch(Exception e)
            {
                throw e;
            }      
        }
    }
}