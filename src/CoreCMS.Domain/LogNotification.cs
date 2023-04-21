using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace CoreCMS.Domain
{
   public class LogNotification
    {
        [Key]
        public int Id { get; set; }
        public string ResponeJobId { get; set; }
        public bool Status { get; set; }
        public bool InActiveStatus { get; set; }
        public long SetTimeSend { get; set; }
        [Required]
        [ForeignKey("Device")]
        public int DeviceId { get; set; }
        [JsonIgnore]
        public virtual Device Device { get; set; }
        [Required]
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        [JsonIgnore]
        public virtual Notification Notification { get; set; }
    }
}
