namespace CoreCMS.Application.Place.Models
{
    public class PlaceQRCode
    {
        public PlaceQRCode()
        {
            MimeType = "image/jpeg";
        }

        public string MimeType { get; set; }
        public string FileName { get; set; }
        public byte[] FileByte { get; set; }
    }
}