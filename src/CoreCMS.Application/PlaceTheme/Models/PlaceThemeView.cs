using System;
using System.Collections.Generic;

namespace CoreCMS.Application.PlaceTheme.Models
{
    public class PlaceThemeView
    {
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; }
        public int ThemeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PictureView> Pictures { get; set; }
        public bool IsSelect { get; set; }
    }

    public class PictureView
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string DownloadFileBase64Url { get; set; }
        public string DownloadFileByteUrl { get; set; }
    }
}