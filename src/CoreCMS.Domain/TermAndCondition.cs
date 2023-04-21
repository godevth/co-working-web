using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreCMS.Domain
{
    public class TermAndCondition : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int TermId { get; set; }

        public string Name { get; set; }

        public string TermTH { get; set; }

        public string TermEN { get; set; }

        [Required]
        public Guid PlaceId { get; set; }

        public virtual Place Place { get; set; }
    }
}
