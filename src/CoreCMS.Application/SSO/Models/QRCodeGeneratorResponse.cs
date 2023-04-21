using System.Net;
using Newtonsoft.Json;

namespace CoreCMS.Application.SSO.Models
{
    public class QRCodeGeneratorResponse
    {
        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("Base64")]
        public string Base64 { get; set; }
    }
}