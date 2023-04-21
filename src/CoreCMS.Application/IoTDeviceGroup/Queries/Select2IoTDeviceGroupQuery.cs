using System;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.IoTDeviceGroup.Queries
{
    public class Select2IoTDeviceGroupQuery : IRequest<CommandResult<OptionViewModel[]>>
    {
        public string Search { get; set; }

        public Guid? RoomId { get; set; }

        [Required]
        public int BookingId { get; set; }

        public bool IsSuperAdmin { get; set; }

        public string UserId { get; set; }
    }
}