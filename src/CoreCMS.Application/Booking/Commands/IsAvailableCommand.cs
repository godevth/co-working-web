using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class IsAvailableCommand : IRequest<bool>
    {
        [Required]
        public Guid RoomId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        public int? BookingId { get; set; }
    }
}