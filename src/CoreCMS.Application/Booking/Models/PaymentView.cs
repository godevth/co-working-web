using System;

namespace CoreCMS.Application.Booking.Models
{
    public class PaymentView
    {
        public string PaymentMethodCode { get; set; }
        
        public string PaymentMethodName { get; set; }

        public DateTime PaymentDate { get; set; }

        public string ChannelCode { get; set; }

        public string ChannelName { get; set; }

        public string ReferenceNo { get; set; }

        public long PaymentDateUnix
        { 
            get
            {
                return ((DateTimeOffset)PaymentDate).ToUnixTimeMilliseconds();
            }
        }
    }
}