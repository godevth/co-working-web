using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
   public class Device
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("UserOwner")]
        public string UserOwnerId { get; set; }
        public string Token { get; set; }
        public string AppId { get; set; }
        public string OS { get; set; }
        public string ResponeDeviceId { get; set; }
        public bool InActiveStatus { get; set; }
        public string Language { get; set; }
        public virtual ApplicationUser UserOwner { get; set; }
        public virtual ICollection<Badge> Badge { get; set; }
        public virtual ICollection<LogNotification> LogNotification { get; set; }
    }
    public static class DeviceType
    {
        public static string Android = "Android";
        public static string iOS = "iOS";
        public static List<string> AllType = new List<string>() { Android, iOS };
    }
}
