using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreCMS.Domain
{
   public class Room : ManipulateModel<ApplicationUser>
    {
        [Key]
        public Guid RoomId { get; set; }
        public string RoomName_TH { get; set; }
        public string RoomName_EN { get; set; }
        public string Detail_TH { get; set;}
        public string Detail_EN { get; set;}
        //public int RoomQty { get; set; }
        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }
        //public decimal Price { get; set; }
        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }
        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }
        public virtual Place Place { get; set; }
        public virtual ICollection<FacilityServices> FacilityServices { get; set; }
        public virtual ICollection<RoomPrice> RoomPrice { get; set; }
    }
}
