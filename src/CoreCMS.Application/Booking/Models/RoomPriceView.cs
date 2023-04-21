namespace CoreCMS.Application.Booking.Models
{
    public class RoomPriceView
    {
        public int Id { get; set; }
        public string TimeType { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public string TimeTypeText
        { 
            get
            {
                string text = "";
                if(TimeType == "Hours")
                    text = "HOURLY";
                else if(TimeType == "Day")
                    text = "DAILY";
                else if(TimeType == "Month")
                    text = "MONTHLY";
                return text;
            }
        }
    }
}