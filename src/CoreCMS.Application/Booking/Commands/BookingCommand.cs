using System;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;


namespace CoreCMS.Application.Booking.Commands
{
    public class BookingCommand : BookingForm, IRequest<CommandResult<string>>
    {
    }
}