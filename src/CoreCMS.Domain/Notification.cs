using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace CoreCMS.Domain
{
   public class Notification : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NotiCode { get; set; }
        [Required]
        //[ForeignKey("NotiType")]
        public string NotiTypeCode { get; set; }
        [Required]
        [ForeignKey("UserOwner")]
        public string UserOwnerId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser UserOwner { get; set; }
        //[JsonIgnore]
        //public virtual SystemVariable NotiType { get; set; }
        public virtual ICollection<LogNotification> LogNotification { get; set; }
        public virtual ICollection<MessageNotification> Messages { get; set; }
    }
    public static class Language
    {
        public static string TH = "TH";
        public static string EN = "EN";
        public static List<string> AllLanguage = new List<string>() { TH, EN };
    }
}
