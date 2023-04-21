using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
   public class FacilityServices : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int FacilityServicesId { get; set; }
        [ForeignKey("Room")]
        public Guid? RoomId { get; set; }
        public virtual Room Room { get; set; }
        [ForeignKey("Place")]
        public Guid? PlaceId { get; set; }
        public virtual Place Place { get; set; }
        [ForeignKey("Facility")]
        public Guid FacilityId { get; set; }
        public virtual Facility Facility { get; set; }
        [Range(0, int.MaxValue)]
        public int Qty { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

    }
}
