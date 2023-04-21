using Newtonsoft.Json;

namespace CoreCMS.Application.ApiMasterData.Models
{
    public class MasterTumbol
    {
        [JsonProperty("TUMBOL_ID")]
        public string TumbolId { get; set; }

        [JsonProperty("TUMBOL_TH")]
        public string TumbolTH { get; set; }

        [JsonProperty("TUMBOL_EN")]
        public string TumbolEN { get; set; }

        [JsonProperty("PROVINCE_ID")]
        public string ProvinceId { get; set; }

        [JsonProperty("AMPHER_ID")]
        public string AmpherId { get; set; }

        [JsonProperty("POSTCODE")]
        public string PostCode { get; set; }
    }
}