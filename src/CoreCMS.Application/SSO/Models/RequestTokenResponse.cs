
using System;
using Newtonsoft.Json;

namespace CoreCMS.Application.SSO.Models
{
    public class RequestTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public Int16 ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}