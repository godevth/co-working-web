using CoreCMS.Application.Payment.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Payment.Commands
{
    public class PaymentBookingCommand : PaymentForm, IRequest<CommandResult<int>>
    {
        public string CreateUserId { get; set; }
    }
}