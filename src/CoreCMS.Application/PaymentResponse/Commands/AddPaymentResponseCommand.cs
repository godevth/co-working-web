using MediatR;

namespace CoreCMS.Application.PaymentResponse.Commands
{
    public class AddPaymentResponseCommand : IRequest<bool>
    {
        public string Payload { get; set; }
        //public PaymentResponseModel Obj { get; set; }
    }
}
