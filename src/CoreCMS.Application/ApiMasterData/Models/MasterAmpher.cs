using Newtonsoft.Json;

namespace CoreCMS.Application.ApiMasterData.Models
{
    public class MasterAmpher
    {
        [JsonProperty("AMPHUR_ID")]
        public string AmpherId { get; set; }

        [JsonProperty("AMPHUR_TH")]
        public string AmpherTH { get; set; }

        [JsonProperty("AMPHUR_EN")]
        public string AmpherEN { get; set; }

        [JsonProperty("PROVINCE_ID")]
        public string ProvinceId { get; set; }

        [JsonProperty("PROVINCE_TH")]
        public string ProvinceTH { get; set; }

        [JsonProperty("PROVINCE_EN")]
        public string ProvinceEN { get; set; }
    }
}