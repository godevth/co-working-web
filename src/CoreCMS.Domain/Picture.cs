using CoreCMS.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Domain
{
    public class Picture : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int PictureId { get; set; }

        [StringLength(50)]
        public string CodeRef { get; set; }

        [StringLength(50)]
        public string TypeRef { get; set; }

        [StringLength(100)]
        [Required]
        public string FileName { get; set; }

        [StringLength(100)]
        [Required]
        public string FileInfoId { get; set; }

        [StringLength(100)]
        [Required]
        public string GridFsId { get; set; }

        [StringLength(50)]
        [Required]
        public string SysName { get; set; }

        [StringLength(500)]
        [Required]
        public string DownloadUrl { get; set; }
    }

    public static class PictureTypeRef
    {
        public static string User = "User";
        public static string Room = "Room";
        public static string Facility = "Facility";
        public static string PaymentSlip = "PaymentSlip";
        public static string RoomType = "RoomType";
        public static string ThemeTypeBgDark = "THEME_TYPE_BG_DARK";
        public static string ThemeTypeBgLight = "THEME_TYPE_BG_LIGHT";
        public static string ThemeTypeLogoDark = "THEME_TYPE_LOGO_DARK";
        public static string ThemeTypeLogoLight = "THEME_TYPE_LOGO_LIGHT";
        public static string UploadFileByPlace ="FILE_BY_PLACE";
    }
}
