using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class CheckInCommand : IRequest<CommandResult<int>>
    {
        public int BookingId { get; set; }
        public string CreateUserId { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}