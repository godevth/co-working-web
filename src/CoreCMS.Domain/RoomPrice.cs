using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
   public class RoomPrice : ManipulateModel<ApplicationUser>
    {
        public int Id { get; set; }
        public string TimeType { get; set; }
        [Range(0, int.MaxValue)]
        public int Qty { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [ForeignKey("Room")]
        public Guid RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}
