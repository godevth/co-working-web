using CoreCMS.Application.Booking.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Booking.Queries
{
    public class GetBookingQuery : IRequest<BookingView>
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
