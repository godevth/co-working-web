using System;
using System.Collections.Generic;
using System.Text;
using CoreCMS.Data;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.Booking.Models
{
   public class BookingDetailView
    {
        public string RoomName { get; set; }
        public string PlaceName { get; set; }
        public string BookingNumber { get; set; } 
        public string BookingStatusCode { get; set; }   
        public string BookingStatusName { get; set; } 
        public string BookingStatusColor { get; set; } 
        public string BookingStatusIconName { get; set; } 
        public int Capacity { get; set; }
        public string OwnerId { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string OwnerPhoneCountryCode { get; set; }
        public string Address { get; set; }
        public int? TambolId { get; set; }
        public int? AmpherId { get; set; }
        public int? ProvinceId { get; set; }
        public string TambolName { get; set; }
        public string AmpherName { get; set; }
        public string ProvinceName { get; set; }
        public string Zipcode { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public RoomPriceView RoomPrice { get; set; }
        public int? BookingId { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public Guid RoomId { get; set; }
        public string Remark { get; set; }
        public bool IsOwner { get; set; }
        public string CustomerId { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodName { get; set; }
        public Guid PlaceId { get; set; }
        public decimal Qty { get; set; }
        public int RoomPriceId { get; set; }
        public string TimeType { get; set; }
        public decimal Discount { get; set; }
        public List<BookingFacilityView> BookingFacilities { get; set; }
        public PaymentView LastPayment { get; set; }

        public long BookingStartDateUnix
        { 
            get
            {
                return ((DateTimeOffset)BookingStartDate).ToUnixTimeMilliseconds();
            }
        }

        public long BookingEndDateUnix
        { 
            get
            {
                return ((DateTimeOffset)BookingEndDate).ToUnixTimeMilliseconds();
            }
        }
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
