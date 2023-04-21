using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
   public class ImplementationDate : ManipulateModel<ApplicationUser>
    {
        public int ImplementationDateId { get; set; }
        public string StartDay { get; set; }
        public string EndDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
