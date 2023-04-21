namespace CoreCMS.Application.Picture.Models
{
    public class PictureView
    {
        public int PictureId { get; set; }

        public string CodeRef { get; set; }

        public string TypeRef { get; set; }

        public string FileName { get; set; }

        public string FileInfoId { get; set; }
        
        public string GridFsId { get; set; }

        public string SysName { get; set; }

        public string DownloadUrl { get; set; }
    }
}