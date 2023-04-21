using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
    public class UserTheme : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int UserThemeId { get; set; }

        public string UserId { get; set; }

        [ForeignKey ("PlaceTheme")]
        public int? ThemeId { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        public virtual PlaceTheme PlaceTheme { get; set; }
    }
}