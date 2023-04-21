using System;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Data;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace CoreCMS.Application.ApiIoT.Commands
{
    public class IoTControlCommandHandler : IRequestHandler<IoTControlCommand, bool>
    {
        private readonly ApiConfig _config;
        private readonly IMediator _mediator;

        public IoTControlCommandHandler(IOptions<ApiConfig> config, IMediator mediator)
        {
            _config = config.Value;
            _mediator = mediator;
        }

        public async Task<bool> Handle(IoTControlCommand request, CancellationToken cancellationToken)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            RestClient cli = new RestClient(_config.IoTControl);

            RestRequest req = new RestRequest("", Method.POST);

            //Header
            req.AddHeader("Content-Type", "application/json");
            //req.AddHeader("Authorization", "Bearer " + _config.AccessToken);

            //Body
            req.RequestFormat = DataFormat.Json;
            req.AddJsonBody(
                new{
                    Device_ID = request.MongoDeviceId,
                    Config = request.Config,
                    Dimmer = request.Dimmer.HasValue ? request.Dimmer.Value : (int?)null,
                    Status = "1"
                }
            );

            bool check = false;
            var result = cli.Execute(req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                check = true;
            }
            // else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            // {
            //     await RequestTokenSSO();
            //     await _mediator.Send(request);   
            // }
            else 
            {
                if (!string.IsNullOrEmpty(result.Content))
                {
                    throw new Exception(result.Content.ToString());
                }
            }
            return await Task.FromResult(check);
        }
        
    }
}