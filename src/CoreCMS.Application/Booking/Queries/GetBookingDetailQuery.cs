using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Booking.Queries
{
   public class GetBookingDetailQuery: IRequest<CommandResult<BookingDetailView>>
    {
        public int BookingId { get; set; }
        public string Language { get; set; }
        public string BookingNumber { get; set; } 
    }
}
