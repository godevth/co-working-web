using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Domain
{
    public class BookingDate
    {
        [Key]
        public int BookingDateId { get; set; }

        [Required]
        public int BookingId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string OpenDay { get; set; }

        public string OpenTime { get; set; }

        public string CloseTime { get; set; }

        public bool IsCheckIn { get; set; }

        public bool IsCheckOut { get; set; }

        public virtual Booking Booking { get; set; }
        
    }
}