using System.Net;
using Newtonsoft.Json;

namespace CoreCMS.Application.ApiMasterFile.Models
{
    public class UploadFileResponse
    {
        [JsonProperty("requestDate")]
        public string RequestDate { get; set; }

        [JsonProperty("httpStatusCode")]
        public HttpStatusCode HttpStatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public UploadFileResult Result { get; set; }
    }
}