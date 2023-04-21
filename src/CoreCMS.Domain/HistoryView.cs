using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
   public class HistoryView : PlaceRoomView
    {
        public int BookingId { get; set; }
        public int BookingRunning { get; set; }
        public string BookingNumber { get; set; }

        [ForeignKey("PaymentMethod")]
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodName{ get; set; }
        public string Remark { get; set; }
        public bool IsOwner { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerId { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        
        [ForeignKey("BookingStatus")]
        public string BookingStatusCode { get; set; }
        public string SystemVariableName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string OwnerId { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string OwnerPhoneCountryCode { get; set; }
        public bool IsDeleted { get; set; }
        public bool InActiveStatus { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public string PriceTimeType { get; set; }
        public int PriceQty { get; set; }
        public decimal Price { get; set; }
        public decimal Remaining { get; set; }

        [ForeignKey("Void")]
        public string VoidCode { get; set; }

        [ForeignKey ("RefundedUser")]
        public string RefundedBy { get; set; }
        public DateTime? RefundedDate { get; set; }

        public virtual SystemVariable PaymentMethod { get; set; }
        public virtual SystemVariable BookingStatus { get; set; }
        public virtual SystemVariable Void { get; set; }
        public virtual ApplicationUser RefundedUser { get; set; }
    }
}
