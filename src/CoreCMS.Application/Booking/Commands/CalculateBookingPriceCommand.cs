using System.Collections.Generic;
using CoreCMS.Application.Booking.Models;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class CalculateBookingPriceCommand : IRequest<CalculateBookingPrice>
    {
        public CalculateBookingPriceCommand()
        {
            FacilityPrices = new List<FacilityPrice>();
        }

        public int Qty { get; set; }
        public double Price { get; set; }
        public int Vat { get; set; }
        public double Discount { get; set; }
        public List<FacilityPrice> FacilityPrices { get; set; }
    }
}