using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.Shared.Models;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Data;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.SSO.Commands
{
    public class QRCodeGeneratorCommandHandler : IRequestHandler<QRCodeGeneratorCommand, CommandResult<Response<List<QRCodeGeneratorResponse>>>>
    {
        private readonly ApiConfig _config;
        private readonly IMediator _mediator;

        public QRCodeGeneratorCommandHandler(IOptions<ApiConfig> config, IMediator mediator)
        {
            _config = config.Value;
            _mediator = mediator;
        }

        public async Task<CommandResult<Response<List<QRCodeGeneratorResponse>>>> Handle(QRCodeGeneratorCommand request, CancellationToken cancellationToken)
        {
            CommandResult<Response<List<QRCodeGeneratorResponse>>> cmdResult = new CommandResult<Response<List<QRCodeGeneratorResponse>>>()
            {
                Message = "ไม่สามารถเรียก API QRCodeGenerator ได้"
            };
            Response<List<QRCodeGeneratorResponse>> response = null;

            //Request Token From SSO
            await RequestTokenSSO();

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            RestClient cli = new RestClient(_config.QRCodeGenerator);

            RestRequest req = new RestRequest("", Method.POST);

            //Header
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + _config.AccessToken);

            //Parameter
            req.RequestFormat = DataFormat.Json;
            req.AddJsonBody(
                new { 
                    QRInfolist = request.QRInfos
                }
            );

            var result = cli.Execute(req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(result.Content))
                {
                    response = JToken.Parse(result.Content).ToObject<Response<List<QRCodeGeneratorResponse>>>();
                }

                cmdResult.Message = "เรียก API QRCodeGenerator สำเร็จ";
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
