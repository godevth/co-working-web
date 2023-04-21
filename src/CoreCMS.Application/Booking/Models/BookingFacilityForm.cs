using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CoreCMS.Application.Shared.JsonConverters;

namespace CoreCMS.Application.Booking.Models
{
    public class BookingFacilityForm
    {
        public int FacilityServicesId { get; set; }
        public string FacilityName { get; set; }
        public int Qty { get; set; }
        public string Price { get; set; }
        public bool IsAll { get; set; }
    }
}