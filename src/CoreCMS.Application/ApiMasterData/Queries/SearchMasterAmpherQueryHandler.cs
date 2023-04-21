using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterData.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.Shared.Models;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Data;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace CoreCMS.Application.ApiMasterData.Queries
{
    public class SearchMasterAmpherQueryHandler : IRequestHandler<SearchMasterAmpherQuery, CommandResult<List<MasterAmpher>>>
    {
        private readonly ApiConfig _config;
        private readonly IMediator _mediator;

        public SearchMasterAmpherQueryHandler(IOptions<ApiConfig> config, IMediator mediator)
        {
            _config = config.Value;
            _mediator = mediator;
        }

        public async Task<CommandResult<List<MasterAmpher>>> Handle(SearchMasterAmpherQuery request, CancellationToken cancellationToken)
        {
            CommandResult<List<MasterAmpher>> cmdResult = new CommandResult<List<MasterAmpher>>()
            {
                Message  = "ไม่สามารถเรียก API Ampher ได้"
            };
            Response<List<MasterAmpher>> response = null;

            //Request Token From SSO
            await RequestTokenSSO();

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var url = _config.AmphurUrl;
            RestClient cli = new RestClient(url);

            var method = Method.POST;
            RestRequest req = new RestRequest("", method);

            //Header
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + _config.AccessToken);

            //Body
            req.RequestFormat = DataFormat.Json;
            req.AddJsonBody(
                new 
                {
                    P_AMPHUR_ID = !string.IsNullOrEmpty(request.AmpherId) ? request.AmpherId : "",
                    P_PROVINCE_ID = !string.IsNullOrEmpty(request.ProvinceId) ? request.ProvinceId : ""
                }
            );

            var result = cli.Execute(req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(result.Content))
                {
                    response = JToken.Parse(result.Content).ToObject<Response<List<MasterAmpher>>>();
                }

                cmdResult.Message = "เรียก API Ampher สำเร็จ";
                cmdResult.Data = response.Result;
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
            catch(Exception e)
            {
                throw e;
            }      
        }
    }
}