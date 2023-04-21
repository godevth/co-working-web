using System.Threading.Tasks;
using CoreCMS.Domain;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class CheckBookingRoleCommand : IRequest<HistoryView>
    {
        public int BookingId { get; set; }

        public bool IsSuperAdmin { get; set; }

        public string UserId { get; set; }

        public string TypeUse { get; set; }
    }
}