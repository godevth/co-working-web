using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
   public class Company : ManipulateModel<ApplicationUser>
    {
        public int CompanyId { get; set; }
        public string CompanyName_TH { get; set; }
        public string CompanyName_EN { get; set; }
        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<CompanyProfiles> CompanyProfiles { get; set; }

    }
}
