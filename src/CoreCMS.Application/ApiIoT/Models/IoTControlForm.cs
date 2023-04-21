using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Application.ApiIoT.Models
{
    public class IoTControlForm
    {
        [Required]
        public string MongoDeviceId { get; set; } 

        [Required]
        public string Config { get; set; } 

        public int? Dimmer { get; set; } 
    }
}