using System;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class IsRoleOwnerAdminPlaceCommand : IRequest<bool>
    {
        public Guid PlaceId { get; set; }
        public string UserId { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}