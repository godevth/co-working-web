using System;

namespace CoreCMS.Application.Booking.Models
{
    public class BookingDateView
    {
        public int BookingDateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string OpenDay { get; set; }

        public string OpenTime { get; set; }

        public string CloseTime { get; set; }

        public string StartDateString
        {
            get
            {
                return StartDate.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public string EndDateString
        {
            get
            {
                return EndDate.ToString("dd/MM/yyyy HH:mm");
            }
        }
    }
}