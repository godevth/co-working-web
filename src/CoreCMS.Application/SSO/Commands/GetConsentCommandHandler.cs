using System;
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
    public class GetConsentCommandHandler : IRequestHandler<GetConsentCommand, CommandResult<GetConsentResponse>>
    {
        private readonly ConsentConfig _config;
        private readonly IMediator _mediator;

        public GetConsentCommandHandler(IOptions<ConsentConfig> config, IMediator mediator)
        {
            _config = config.Value;
            _mediator = mediator;
        }

        public async Task<CommandResult<GetConsentResponse>> Handle(GetConsentCommand request, CancellationToken cancellationToken)
        {
            CommandResult<GetConsentResponse> cmdResult = new CommandResult<GetConsentResponse>()
            {
                Message = "ไม่สามารถเรียก API GetConsent ได้"
            };
            GetConsentResponse response = null;

            //Request Token From SSO
            await RequestTokenSSO();

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            RestClient cli = new RestClient(_config.GetConsentUrl);

            RestRequest req = new RestRequest("", Method.POST);

            //Header
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddHeader("Authorization", "Bearer " + _config.AccessToken);

            //Body
            req.AddParameter("ClientId", _config.ClientIdRequest, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            req.AddParameter("ScopesRequest", _config.ScopesRequest, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            req.AddParameter("IsAnotherProvider", true, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            if(!string.IsNullOrEmpty(request.UserId))
            {
                req.AddParameter("UserId", request.UserId, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            }

            var result = cli.Execute(req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(result.Content))
                {
                    response = JToken.Parse(result.Content).ToObject<GetConsentResponse>();
                }

                cmdResult.Message = "เรียก API GetConsent สำเร็จ";
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