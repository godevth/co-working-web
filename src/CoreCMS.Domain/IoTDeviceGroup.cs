using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
    public class IoTDeviceGroup : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int IoTDeviceGroupId { get; set; }

        [Required]
        public string GroupName { get; set; } 

        public string Description { get; set; } 
        
        public bool IsOpenPerDevice { get; set; }
        public Guid RoomId { get; set; }
        
        public virtual Room Room { get; set; }
        public virtual ICollection<IoTDevice> IoTDevices { get; set; }
    }
}