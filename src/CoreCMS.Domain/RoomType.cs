using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreCMS.Domain
{
   public class RoomType : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int RoomTypeID { get; set; }
        public string RoomTypeName{ get; set; }
        public string RoomTypeNameEN{ get; set; }
    }
}
