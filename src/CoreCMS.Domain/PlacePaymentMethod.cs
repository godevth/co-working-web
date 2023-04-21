using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCMS.Domain
{
    public class PlacePaymentMethod
    {
        [Key]
        public int PlacePaymentMethodId { get; set; }

        [Required]
        public Guid PlaceId { get; set; }

        [ForeignKey ("PaymentMethod")]
        public string PaymentMethodCode { get; set; }

        public virtual Place Place { get; set; }
        public virtual SystemVariable PaymentMethod { get; set; }
    }
}