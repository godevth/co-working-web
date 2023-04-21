using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
   public class Place : ManipulateModel<ApplicationUser>
    {
        [Key]
        public Guid PlaceId { get; set; }
        [StringLength(450),Required]
        public string PlaceName_TH { get; set; }
        public string PlaceName_EN { get; set; }
        public string Address { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        [StringLength(10)]
        public int? TAMBON_ID { get; set; }

        [StringLength(10)]
        public int? AMPER_ID { get; set; }

        [StringLength(10)]
        public int? PROVINCE_ID { get; set; }
        [StringLength(5)]
        public string ZIP_CODE { get; set; }
        public string Details { get; set; }
        public string NearBy { get; set; }
        [ForeignKey("PlaceType")]
        public int PlaceTypeID { get; set; }
        public virtual PlaceType PlaceType { get; set; }
        [ForeignKey("Company")]
        public int? CompanyId { get; set; }

        [ForeignKey("SeeingType")]
        public string SeeingTypeCode { get; set; }

        public bool IsApproveCreate { get; set;}
        
        public bool IsApproveDelete { get; set; }

        public string SubMerchantId { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal GP { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<ImplementationDate> ImplementationDate { get; set; }
        public virtual ICollection<FacilityServices> FacilityServices { get; set; }
        public virtual ICollection<PlacePaymentMethod> PlacePaymentMethods { get; set; }
        public virtual ICollection<Privilege> Privileges { get; set; }
        public virtual SystemVariable SeeingType { get; set; }
    }
}
