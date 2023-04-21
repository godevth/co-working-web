using System;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Shared.JsonConverters;
using Newtonsoft.Json;

namespace CoreCMS.Application.Payment.Models
{
    public class PaymentForm
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public string PaymentMethodCode { get; set; }

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

        //GMT0
        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime PaymentDate { get; set; }

        public string BankCode { get; set; }

        public string CreditCardTypeCode { get; set; }

        public string MerchantId { get; set; }

        public string RefCode { get; set; }

        public string TransactionId { get; set; }

        public string Picture { get; set; }
        
        public string ContentType { get; set; }

        public DateTime PaymentDateLocal 
        { 
            get
            {
                return PaymentDate.ToLocalTime();
            }
        }
        public bool IsOnlinePayment { get; set; }
        public int? PaymentResponseId { get; set; }
    }
}