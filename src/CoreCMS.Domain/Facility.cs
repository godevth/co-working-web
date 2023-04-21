using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
   public class Facility : ManipulateModel<ApplicationUser>
    {
        [Key]
        public Guid FacilityId { get; set; }
        public string FacilityName_TH { get; set; }
        public string FacilityName_EN { get; set; }
        [ForeignKey("FacilityType")]
        public int FacilityTypeID { get; set; }
        public virtual FacilityType FacilityType { get; set; }
    }
}
