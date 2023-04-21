using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class CheckOutAllCommand : IRequest<CommandResult<int>>
    {
    }
}