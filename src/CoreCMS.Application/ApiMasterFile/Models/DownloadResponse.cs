namespace CoreCMS.Application.ApiMasterFile.Models
{
    public class DownloadResponse
    {
        public string ContentType { get; set; }
        public byte[] DataFile { get; set; }
    }
}