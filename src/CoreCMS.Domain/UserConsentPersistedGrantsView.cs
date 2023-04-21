using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
    public class UserConsentPersistedGrantsView
    {
        public int UserConsentId { get; set; }

        public string UserId { get; set; }

        public Guid PlaceId { get; set; }

        public bool InActiveStatus { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey ("TermAndCondition")]
        public int TermId { get; set; }

        public string PersistedGrantsKey { get; set; }

        public string Type { get; set; }

        public string ClientId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? Expiration { get; set; }

        public string Data { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Place Place { get; set; }
        public virtual TermAndCondition TermAndCondition { get; set; }
    }
}
