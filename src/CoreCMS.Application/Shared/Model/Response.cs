using System.Net;
using Newtonsoft.Json;

namespace CoreCMS.Application.Shared.Models
{
    public class Response<T>
    {
        [JsonProperty("requestDate")]
        public string RequestDate { get; set; }

        [JsonProperty("httpStatusCode")]
        public HttpStatusCode HttpStatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
    }
}