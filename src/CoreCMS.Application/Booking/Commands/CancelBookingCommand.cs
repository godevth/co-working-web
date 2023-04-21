using CoreCMS.Application.Shared.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Booking.Commands
{
   public class CancelBookingCommand:IRequest<CommandResult<bool>>
    {
        public string Language { get; set; }
        public int BookingId { get; set; }
        public string UpdateUserId { get; set; }
        public bool IsMobile { get; set; }
    }
}
