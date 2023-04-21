namespace CoreCMS.Application.Booking.Models
{
    public class CalculatePricing
    {
        public decimal Total { get; set; }

        public decimal Discount { get; set; }

        public decimal Tax { get; set; }

        public decimal GrandTotal { get; set; }

        public decimal RoomPrice { get; set; }

        public decimal FacilityPrice { get; set; }

        public decimal PricePerUnit { get; set; }
        
        public decimal Qty { get; set; }
        
        public int RoomPriceId { get; set; }

        public decimal HeadTotal { get; set; }
        public decimal SubTotal { get; set; }
    }
}