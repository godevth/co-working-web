using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterFile.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace CoreCMS.Application.ApiMasterFile.Commands
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, CommandResult<UploadFileResponse>>
    {
        private readonly ApiConfig _config;
        private readonly IMediator _mediator;
        private readonly ILogger<UploadFileCommandHandler> _logger;

        public UploadFileCommandHandler(IOptions<ApiConfig> config, IMediator mediator, ILogger<UploadFileCommandHandler> logger)
        {
            _config = config.Value;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<CommandResult<UploadFileResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            CommandResult<UploadFileResponse> cmdResult = new CommandResult<UploadFileResponse>()
            {
                Message  = "ไม่สามารถอัพโหลดไฟล์ได้"
            };
            UploadFileResponse response = null;

            //Request Token From SSO
            await RequestTokenSSO();

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var url = _config.UploadFileUrl;
            RestClient cli = new RestClient(url);

            var method = Method.POST;
            RestRequest req = new RestRequest("", method);

            //Header
            req.AddHeader("Content-Type", "application/octet-stream");
            req.AddHeader("Authorization", "Bearer " + _config.AccessToken);

            //Body
            req.AddParameter(nameof(request.FileName), request.FileName, ParameterType.QueryString);          
            req.AddParameter(nameof(_config.SubId), _config.SubId, ParameterType.QueryString);
            req.AddParameter(nameof(_config.SysName), _config.SysName, ParameterType.QueryString);
            req.AddParameter(nameof(_config.FolderId), _config.FolderId, ParameterType.QueryString);
            if(request.Revision.HasValue)
                req.AddParameter(nameof(request.Revision), request.Revision.Value, ParameterType.QueryString);
            else 
                req.AddParameter(nameof(request.Revision), 1, ParameterType.QueryString);
            
            #region SetContentType 
            if(!string.IsNullOrEmpty(request.ContentType))
            {
                string extension;
                if (!_config.AllowFileExtensionsToDict.TryGetValue(request.ContentType, out extension)) {
                    throw new Exception("ContentType not Support");
                }
                req.AddParameter(nameof(request.ContentType), extension, ParameterType.QueryString);
            }
            #endregion

            #region set ContentType 
            if (request.ContentType.ToLower() == "jpeg" || request.ContentType.ToLower() == "jpg")
            {
                req.AddParameter(nameof(request.ContentType), "Imgpng", ParameterType.QueryString);
            }
            else if (request.ContentType.ToLower() == "png")
            {
                req.AddParameter(nameof(request.ContentType), "Imgjpeg", ParameterType.QueryString);
            }
            else if (request.ContentType.ToLower() == "pdf")
            {
                req.AddParameter(nameof(request.ContentType), "documentPdf", ParameterType.QueryString);
            }
            else if (request.ContentType.ToLower() == "xls")
            {
                req.AddParameter(nameof(request.ContentType), "spreedsheet2013", ParameterType.QueryString);
            }
            else if (request.ContentType.ToLower() == "xlsx")
            {
                req.AddParameter(nameof(request.ContentType), "spreedsheet2015", ParameterType.QueryString);
            }
            else if (request.ContentType.ToLower() == "doc")
            {
                req.AddParameter(nameof(request.ContentType), "documentWord2013", ParameterType.QueryString);
            }
            else if (request.ContentType.ToLower() == "docx")
            {
                req.AddParameter(nameof(request.ContentType), "documentWord2015", ParameterType.QueryString);
            }
            #endregion

            //Body Binary
            req.AddParameter("application/octet-stream", request.Data, ParameterType.RequestBody);

            var result = cli.Execute(req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(result.Content))
                {
                    response = JToken.Parse(result.Content).ToObject<UploadFileResponse>();
                }
                cmdResult.Message = "อัพโหลดไฟล์สำเร็จ";
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
                _logger.LogInformation("Else Scope: Result is {@result}", result);

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