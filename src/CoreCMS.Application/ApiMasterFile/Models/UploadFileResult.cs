using Newtonsoft.Json;

namespace CoreCMS.Application.ApiMasterFile.Models
{
    public class UploadFileResult
    {
        [JsonProperty("fileInfoId")]
        public string FileInfoId { get; set; }

        [JsonProperty("gridFsId")]
        public string GridFsId { get; set; }

        [JsonProperty("sysName")]
        public string SysName { get; set; }

        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }
    }
}