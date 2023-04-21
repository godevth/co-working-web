using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
   public class CompanyProfiles : ManipulateModel<ApplicationUser>
    {
        public int CompanyProfilesId { get; set; }
        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        public virtual ApplicationUser Admin { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
