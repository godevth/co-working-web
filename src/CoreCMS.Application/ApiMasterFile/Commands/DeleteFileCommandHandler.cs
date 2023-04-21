using System;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Data;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace CoreCMS.Application.ApiMasterFile.Commands
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, bool>
    {
        private readonly ApiConfig _config;
        private readonly IMediator _mediator;

        public DeleteFileCommandHandler(IOptions<ApiConfig> config, IMediator mediator)
        {
            _config = config.Value;
            _mediator = mediator;
        }
        
        public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            //Request Token From SSO
            await RequestTokenSSO();
            
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            RestClient cli = new RestClient(_config.DeleteFileUrl);

            RestRequest req = new RestRequest("", Method.DELETE);

            //Header
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + _config.AccessToken);

            //Parameter
            req.AddParameter("Id", request.FileInfoId, ParameterType.QueryString);
            req.AddParameter(nameof(_config.SubId), _config.SubId, ParameterType.QueryString);
            req.AddParameter(nameof(_config.SysName), _config.SysName, ParameterType.QueryString);

            bool check = false;
            var result = cli.Execute(req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                check = true;
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
            return await Task.FromResult(check);
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