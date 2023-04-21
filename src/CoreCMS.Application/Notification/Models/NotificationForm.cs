using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreCMS.Application.Notification.Models
{
   public class NotificationForm
    {
        [Required]
        public string RefCode { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string NotificationType { get; set; }
        [Required]
        public string NotificationLink { get; set; }
        [Required]
        public DateTime SendTimeStamp { get; set; }

        public bool IsAllDevice { get; set; }
        public DateTime SendDateTime { get; set; }
        public List<MessageNotis> MessageNoti { get; set; }
    }
    public class MessageNotis {
        public string Subject { get; set; }
        public string Detail { get; set; }
        public string Language { get; set; }

    }
}
