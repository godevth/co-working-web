using System;
using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Domain
{
    public class BookingFacility
    {
        [Key]
        public int BookingFacilityId { get; set; }

        public int BookingId { get; set; }

        [Required]
        public Guid FacilityId { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Qty { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        public virtual Facility Facility { get; set; }
        public virtual Booking Booking { get; set; }
        
    }
}