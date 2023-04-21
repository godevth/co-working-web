using Newtonsoft.Json;

namespace CoreCMS.Application.ApiMasterData.Models
{
    public class MasterProvince
    {
        [JsonProperty("PROVINCE_ID")]
        public string ProvinceId { get; set; }

        [JsonProperty("PROVINCE_TH")]
        public string ProvinceTH { get; set; }

        [JsonProperty("PROVINCE_EN")]
        public string ProvinceEN { get; set; }
    }
}