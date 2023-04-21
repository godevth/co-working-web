using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using CoreCMS.Application.Shared.JsonConverters;

namespace CoreCMS.Application.Booking.Models
{
    public class BookingForm
    {
        public BookingForm()
        {
            Facilities = new List<FacilityForm>();
            Vat = 7;
        }
        
        [Required]
        public Guid RoomId { get; set; }

        public string TimeType { get; set; }

        //GMT0
        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime BookingStartDate { get; set; }

        //GMT0
        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime BookingEndDate { get; set; }

        public List<FacilityForm> Facilities { get; set; }

        public string Language { get; set; }

        public decimal Discount { get; set; }

        public decimal Vat { get; set; }

        public string CurrentUserId { get; set; }

        [Required]
        public string PaymentMethodCode { get; set; }

        public string Remark { get; set; }

        public bool IsOwner { get; set; }

        public string CustomerId { get; set; }

        public string CustomerFirstname { get; set; }

        public string CustomerLastname { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public int? BookingId { get; set; }

        public DateTime BookingStartDateLocal 
        { 
            get
            {
                //set second = 00
                DateTime d = BookingStartDate.AddSeconds(-1 * BookingStartDate.Second);
                return d.ToLocalTime();
            }
        }

        public DateTime BookingEndDateLocal 
        { 
            get
            {
                //set second = 00
                DateTime d = BookingEndDate.AddSeconds(-1 * BookingEndDate.Second);
                return d.ToLocalTime();
            }
        }
        
    }
}