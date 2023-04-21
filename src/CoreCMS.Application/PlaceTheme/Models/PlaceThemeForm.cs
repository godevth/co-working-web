using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Application.PlaceTheme.Models
{
    public class PlaceThemeForm
    {
        public PlaceThemeForm()
        {
            Pictures = new List<Picture>();
        }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public Guid PlaceId { get; set; }

        [Required]
        public List<Picture> Pictures { get; set; }
    }

    public class Picture
    {
        public string Type { get; set; }
        public string Base64 { get; set; }
        public string ContentType { get; set; }
    }
}