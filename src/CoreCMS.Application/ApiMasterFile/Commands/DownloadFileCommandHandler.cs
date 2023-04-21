using System;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterFile.Models;
using CoreCMS.Application.Picture.Queries;
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
    public class DownloadFileCommandHandler : IRequestHandler<DownloadFileCommand, DownloadResponse>
    {
        private readonly ApiConfig _config;
        private readonly IMediator _mediator;

        public DownloadFileCommandHandler(IOptions<ApiConfig> config, IMediator mediator)
        {
            _config = config.Value;
            _mediator = mediator;
        }

        public async Task<DownloadResponse> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            GetPictureByFileInfoIdQuery getPicture = new GetPictureByFileInfoIdQuery()
            {
                FileInfoId = request.FileInfoId
            };
            var picture = await _mediator.Send(getPicture);
            if(picture == null)
                throw new Exception(string.Format("ไม่พบไฟล์ {0} ในระบบ", request.FileInfoId));

            //Request Token From SSO
            await RequestTokenSSO();
            
            string url = string.Format(picture.DownloadUrl, _config.SubId, true);
            RestClient cli = new RestClient(url);

            var method = Method.GET;
            RestRequest req = new RestRequest("", method);

            //Header
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + _config.AccessToken);

            DownloadResponse response = null;
            var result = cli.Execute(req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (result.RawBytes != null && result.RawBytes.Length > 0)
                {
                    response = new DownloadResponse();
                    response.ContentType = result.ContentType;
                    response.DataFile = new byte[result.RawBytes.Length];
                    response.DataFile = result.RawBytes;
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
            return await Task.FromResult<DownloadResponse>(response);
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