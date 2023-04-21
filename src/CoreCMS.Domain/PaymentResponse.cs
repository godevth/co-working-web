using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCMS.Domain
{
    public class PaymentResponse
    {
        [Key]
        public int Id { get; set; }

        [StringLength(25)]
        [Required]
        public string MerchantID { get; set; }

        [StringLength(20)]
        [Required]
        public string BookingNumber { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal? Amount { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal? McpAmount { get; set; }

        [Column(TypeName = "decimal(12,7)")]
        public decimal? McpFxRate { get; set; }

        [StringLength(3)]
        public string McpCurrencyCode { get; set; }

        [StringLength(3)]
        [Required]
        public string CurrencyCode { get; set; }

        [StringLength(14)]
        [Required]
        public string TransactionDateTime { get; set; }

        public DateTime TransactionDate { get; set; }

        [StringLength(30)]
        [Required]
        public string AgentCode { get; set; }

        [StringLength(30)]
        [Required]
        public string ChannelCode { get; set; }

        public string ChannelCodeName { get; set; }

        [StringLength(6)]
        public string ApprovalCode { get; set; }

        [StringLength(15)]
        public string ReferenceNo { get; set; }

        [StringLength(19)]
        public string Pan { get; set; }
        
        [StringLength(20)]
        public string CardToken { get; set; }

        [StringLength(2)]
        public string IssuerCountry { get; set; }

        [StringLength(2)]
        public string Eci { get; set; }

        [StringLength(2)]
        public string InstallmentPeriod { get; set; }

        [StringLength(1)]
        public string InterestType { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal? InterestRate { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal? InstallmentMerchantAbsorbRate { get; set; }

        [StringLength(20)]
        public string RecurringUniqueID { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal? FxAmount { get; set; }

        [Column(TypeName = "decimal(12,7)")]
        public decimal? FxRate { get; set; }

        [StringLength(3)]
        public string FxCurrencyCode { get; set; }

        [StringLength(255)]
        public string UserDefined1 { get; set; }

        [StringLength(255)]
        public string UserDefined2 { get; set; }

        [StringLength(255)]
        public string UserDefined3 { get; set; }

        [StringLength(255)]
        public string UserDefined4 { get; set; }

        [StringLength(255)]
        public string UserDefined5 { get; set; }

        [StringLength(4)]
        public string RespCode { get; set; }

        [StringLength(255)]
        public string RespDesc { get; set; }

        public DateTime CreateDate { get; set; }

        public int BookingId { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
