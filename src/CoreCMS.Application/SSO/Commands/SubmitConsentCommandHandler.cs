using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Data;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace CoreCMS.Application.SSO.Commands
{
    public class SubmitConsentCommandHandler : IRequestHandler<SubmitConsentCommand, CommandResult<SubmitConsentResponse>>
    {
        private readonly ConsentConfig _config;
        private readonly IMediator _mediator;

        public SubmitConsentCommandHandler(IOptions<ConsentConfig> config, IMediator mediator)
        {
            _config = config.Value;
            _mediator = mediator;
        }

        public async Task<CommandResult<SubmitConsentResponse>> Handle(SubmitConsentCommand request, CancellationToken cancellationToken)
        {
            CommandResult<SubmitConsentResponse> cmdResult = new CommandResult<SubmitConsentResponse>()
            {
                Message = "ไม่สามารถเรียก API SubmitConsent ได้"
            };
            SubmitConsentResponse response = null;

            //Request Token From SSO
            await RequestTokenSSO();

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            RestClient cli = new RestClient(_config.SubmitConsentUrl);

            RestRequest req = new RestRequest("", Method.POST);

            //Header
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + _config.AccessToken);

            //Parameter
            req.RequestFormat = DataFormat.Json;
            req.AddJsonBody(
                new { 
                    UserId = request.UserId,
                    ClientId = _config.ClientIdRequest,
                    ScopesRequest = _config.ScopesRequest,
                    AllowConsent = true,
                    IsAnotherProvider = true,
                    ResourceScopes = new[] {
                        new {
                            Name = _config.ResourceScopeName,
                            Checked = true,
                            Claims = new[] {
                                new {
                                    Name = "email",
                                    Value = request.Email
                                }
                            }
                        }
                    }
                }
            );

            var result = cli.Execute(req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(result.Content))
                {
                    response = JToken.Parse(result.Content).ToObject<SubmitConsentResponse>();
                }

                cmdResult.Message = "เรียก API SubmitConsent สำเร็จ";
                cmdResult.Data = response;
                cmdResult.Status = true;

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
                Authority = _config.Authority
            };
            try
            {
                var res = await _mediator.Send(requestTokenCommand);
                if (res.Status && res.Data != null)
                {
                    _config.AccessToken = res.Data.AccessToken;
                }
                return await Task.FromResult(res);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}