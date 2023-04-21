using System;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
    public class PlaceTheme : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int ThemeId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid PlaceId { get; set; }

        public virtual Place Place { get; set; }
    }
}