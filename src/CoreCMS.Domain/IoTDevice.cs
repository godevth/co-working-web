using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
    public class IoTDevice : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int IoTDeviceId { get; set; }

        [Required]
        public string DeviceName { get; set; } 

        public string Description { get; set; }

        public int IoTDeviceGroupId { get; set; }

        [Required]
        public string MongoDeviceId { get; set; } 

        [ForeignKey ("DeviceStatus")]
        public string StatusCode { get; set; }

        [ForeignKey ("DeviceType")]
        public string DeviceTypeCode { get; set; }

        public int? Dimmer { get; set; }
        

        public virtual IoTDeviceGroup IoTDeviceGroup { get; set; }
        public virtual SystemVariable DeviceStatus { get; set; }
        public virtual SystemVariable DeviceType { get; set; }
    }
}