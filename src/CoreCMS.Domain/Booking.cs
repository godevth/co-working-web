using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
    public class Booking : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int BookingRunning { get; set; } 

        [Required]
        public string BookingNumber { get; set; } 
        
        [Required]
        [ForeignKey("PaymentMethod")]
        public string PaymentMethodCode { get; set; }

        public string Remark { get; set; }

        public bool IsOwner { get; set; }

        public string CustomerFirstname { get; set; }

        public string CustomerLastname { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhoneNumber { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Required]
        public decimal GrandTotal { get; set; }

        [Required]
        [ForeignKey("BookingStatus")]
        public string BookingStatusCode { get; set; }

        [Required]
        public Guid RoomId { get; set; }

        [ForeignKey("RoomPrice")]
        public int? RoomPriceId { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Qty { get; set; }

        public string PriceTimeType { get; set; }

        [Range(0, int.MaxValue)]
        public int PriceQty { get; set; }
        
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public decimal Remaining { get; set; }

        [ForeignKey("Void")]
        public string VoidCode { get; set; }

        [ForeignKey ("RefundedUser")]
        public string RefundedBy { get; set; }
        public DateTime? RefundedDate { get; set; }

        public virtual SystemVariable PaymentMethod { get; set; }
        public virtual SystemVariable BookingStatus { get; set; }
        public virtual Room Room { get; set; }
        public virtual RoomPrice RoomPrice { get; set; }
        public virtual SystemVariable Void { get; set; }
        public virtual ApplicationUser RefundedUser { get; set; }
        public virtual ICollection<BookingDate> BookingDates { get; set; }
        public virtual ICollection<BookingFacility> BookingFacilities { get; set; }
    }
}