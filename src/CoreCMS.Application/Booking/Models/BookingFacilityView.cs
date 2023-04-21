using System;

namespace CoreCMS.Application.Booking.Models
{
    public class BookingFacilityView
    {
        public int BookingFacilityId { get; set; }

        public Guid FacilityId { get; set; }

        public string FacilityName { get; set; }
        
        public int Qty { get; set; }
        
        public decimal Price { get; set; }

        public int FacilityServicesId { get; set; }
        
        public bool IsAll { get; set; }

        public string PriceFormat
        {
            get
            {
                return string.Format("{0:n}", Price);
            }
        }

        public decimal Total
        {
            get
            {
                decimal total = Price * Qty;
                return total;
            }
        }

        public string TotalFormat
        {
            get
            {
                return string.Format("{0:n}", Total);
            }
        }
    }
}