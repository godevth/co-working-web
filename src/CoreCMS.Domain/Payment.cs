using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
    public class Payment : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int ReceiveRunning { get; set; } 

        [Required]
        public string ReceiveNumber { get; set; } 

        public int BookingId { get; set; }

        [Required]
        [ForeignKey("PaymentMethod")]
        public string PaymentMethodCode { get; set; }

        [ForeignKey("CounterPayment")]
        public string CounterPaymentCode { get; set; }

        //ยอดคงเหลือที่ต้องจ่ายในบิลนี้
        [Required]
        public decimal Total { get; set; } 

        //จำนวนเงินที่จ่าย
        [Required]
        public decimal Paid { get; set; }
        
        //เงินที่รับ
        [Required]
        public decimal Receive { get; set; }

        //เงินทอน
        [Required]
        public decimal Change { get; set; }

        //ยอดคงค้าง
        [Required]
        public decimal Remaining { get; set; }

        public DateTime PaymentDate { get; set; }

        [ForeignKey ("ReceiveUser")]
        public string ReceiveUserId { get; set; }

        [ForeignKey("Bank")]
        public string BankCode { get; set; }

        [ForeignKey("CreditCardType")]
        public string CreditCardTypeCode { get; set; }

        public string MerchantId { get; set; }

        public string RefCode { get; set; }

        public string TransactionId { get; set; }

        [ForeignKey("PaymentResponse")]
        public int? PaymentResponseId { get; set; }
        
        public virtual Booking Booking { get; set; }
        public virtual SystemVariable CounterPayment { get; set; }
        public virtual ApplicationUser ReceiveUser { get; set; }
        public virtual SystemVariable Bank { get; set; }
        public virtual SystemVariable CreditCardType { get; set; }
        public virtual SystemVariable PaymentMethod { get; set; }
        public virtual PaymentResponse PaymentResponse { get; set; }
    }
}