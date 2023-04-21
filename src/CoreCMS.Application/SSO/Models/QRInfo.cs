using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CoreCMS.Application.SSO.Models
{
    public class QRInfo
    {
        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }

        [JsonProperty("Width")]
        public int Width { get; set; }

        [JsonProperty("Height")]
        public int Height { get; set; }
    }
}
