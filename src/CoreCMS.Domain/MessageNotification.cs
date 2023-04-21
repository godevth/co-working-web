using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace CoreCMS.Domain
{
   public class MessageNotification
    {
        [Key]
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public string Language { get; set; }
        [Required]
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        [JsonIgnore]
        public virtual Notification Notification { get; set; }
    }
}
