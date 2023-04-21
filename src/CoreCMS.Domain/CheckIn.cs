using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
    public class CheckIn : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int CheckInId { get; set; }

        public int BookingId { get; set; }

        [ForeignKey ("CheckInUser")]
        public string CheckInUserId { get; set; }

        public DateTime? CheckInDate { get; set; }

        [ForeignKey ("CheckOutUser")]
        public string CheckOutUserId { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public virtual ApplicationUser CheckInUser { get; set; }
        public virtual ApplicationUser CheckOutUser { get; set; }

        public virtual Booking Booking { get; set; }
    }
}