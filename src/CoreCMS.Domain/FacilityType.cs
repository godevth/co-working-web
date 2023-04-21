using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreCMS.Domain
{
  public class FacilityType : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int FacilityTypeID { get; set; }
        public string FacilityTypeName_TH { get; set; }
        public string FacilityTypeName_EN { get; set; }
    }
}
