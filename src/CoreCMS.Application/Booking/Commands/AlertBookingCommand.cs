using CoreCMS.Application.Shared.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Booking.Commands
{
   public class AlertBookingCommand : IRequest<CommandResult<bool>>
    {
    }
}
