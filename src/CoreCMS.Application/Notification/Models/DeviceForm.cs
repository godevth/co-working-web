using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Application.Notification.Models
{
    public class DeviceForm
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string AppId { get; set; }
        [Required]
        public string AppName { get; set; }
        [Required]
        public string OS { get; set; }
        [Required]
        public string Language { get; set; }
    }
}