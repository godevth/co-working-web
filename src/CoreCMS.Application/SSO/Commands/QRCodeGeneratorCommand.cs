using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.Shared.Models;
using CoreCMS.Application.SSO.Models;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.SSO.Commands
{
    public class QRCodeGeneratorCommand : IRequest<CommandResult<Response<List<QRCodeGeneratorResponse>>>>
    {
        
        [JsonProperty("QRInfolist")]
        public List<QRInfo> QRInfos { get; set; }
    }
}
