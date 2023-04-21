using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.IoTDevice.Queries
{
    public class Select2IoTDeviceQuery : IRequest<CommandResult<OptionViewModel[]>>
    {
        public string Search { get; set; }

        public int? IoTDeviceGroupId { get; set; }

        [Required]
        public int BookingId { get; set; }

        public bool IsSuperAdmin { get; set; }

        public string UserId { get; set; }
    }
}