using System;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.SSO.Models;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace CoreCMS.Application.SSO.Commands
{
    public class RequestTokenCommandHandler : IRequestHandler<RequestTokenCommand, CommandResult<RequestTokenResponse>>
    {
        public async Task<CommandResult<RequestTokenResponse>> Handle(RequestTokenCommand request, CancellationToken cancellationToken)
        {
            CommandResult<RequestTokenResponse> cmdResult = new CommandResult<RequestTokenResponse>();
            RequestTokenResponse requestTokenResponse = null;

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var url = request.AccessTokenForNotification ? request.AuthoritySSO : request.Authority;
            RestClient cli = new RestClient(url);

            var method = Method.POST;
            RestRequest req = new RestRequest("", method);

            //Header
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            //Body
            req.AddParameter("scope", request.Scope, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            req.AddParameter("client_id", request.ClientId, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            req.AddParameter("client_secret", request.ClientSecret, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            req.AddParameter("grant_type", request.GrantType, "application/x-www-form-urlencoded", ParameterType.GetOrPost);

            var result = cli.Execute(req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(result.Content))
                {
                    requestTokenResponse = JToken.Parse(result.Content).ToObject<RequestTokenResponse>();
                }
                cmdResult.Data = requestTokenResponse;
                cmdResult.Status = true;
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
    }
}