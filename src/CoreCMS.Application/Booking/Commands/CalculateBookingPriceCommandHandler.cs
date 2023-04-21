using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class CalculateBookingPriceCommandHandler : BaseDbContextHandler<CalculateBookingPriceCommand, CalculateBookingPrice>
    {
        public CalculateBookingPriceCommandHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<CalculateBookingPrice> Handle(CalculateBookingPriceCommand request, CancellationToken cancellationToken)
        {
            double roomPrice = request.Qty * request.Price;
            double facilityPrice = 0;
            foreach(var fp in request.FacilityPrices)
            {
                facilityPrice += fp.Qty * fp.Price;
            }

            CalculateBookingPrice price = new CalculateBookingPrice();
            price.Total = (roomPrice + facilityPrice) - request.Discount;
            price.Discount = request.Discount;
            price.Tax = (price.Total / 100) * request.Vat;
            price.GrandTotal = (price.Total + price.Tax);

            return await Task.FromResult(price);
        }
    }
}