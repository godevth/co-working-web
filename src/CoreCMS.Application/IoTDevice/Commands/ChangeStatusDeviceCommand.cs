using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.IoTDevice.Commands
{
    public class ChangeStatusDeviceCommand : IRequest<CommandResult<string>>
    {
        public int? IoTDeviceId { get; set; }

        [Required]
        public int IoTDeviceGroupId { get; set; }
        
        [Required]
        public string StatusCode { get; set; }

        public int? Dimmer { get; set; }
        
        [Required]
        public int BookingId { get; set; }

        public bool IsSuperAdmin { get; set; }

        public string CreateUserId { get; set; }
    }
}