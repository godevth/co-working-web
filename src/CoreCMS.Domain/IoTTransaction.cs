using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
    public class IoTTransaction : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int IoTTransactionId { get; set; }

        public int IoTDeviceGroupId { get; set; }

        public int? IoTDeviceId { get; set; }

        [ForeignKey ("IoTDeviceStatus")]
        public string StatusCode { get; set; }

        public int? Dimmer { get; set; }

        
        public virtual IoTDeviceGroup IoTDeviceGroup { get; set; }

        public virtual IoTDevice IoTDevice { get; set; }

        public virtual SystemVariable IoTDeviceStatus { get; set; }
    }
}