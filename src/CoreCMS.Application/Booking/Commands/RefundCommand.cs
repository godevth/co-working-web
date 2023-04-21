using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class RefundCommand : IRequest<CommandResult<bool>>
    {
        public int BookingId { get; set; }
        public string CurrentUser { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}