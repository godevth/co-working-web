namespace CoreCMS.Application.Booking.Models
{
    public class CalculateBookingPrice
    {
        public double Total { get; set; }

        public double Discount { get; set; }

        public double Tax { get; set; }

        public double GrandTotal { get; set; }
    }
}