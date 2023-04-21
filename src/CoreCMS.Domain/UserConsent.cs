using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
    public class UserConsent : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int UserConsentId { get; set; }

        public string PersistedGrantsKey { get; set; }

        public string UserId { get; set; }

        [Required]
        public Guid PlaceId { get; set; }

        [ForeignKey ("TermAndCondition")]
        public int TermId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Place Place { get; set; }
        public virtual TermAndCondition TermAndCondition { get; set; }
    }
}
