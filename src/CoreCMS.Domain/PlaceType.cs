using CoreCMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreCMS.Domain
{
   public class PlaceType : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int PlaceTypeID { get; set; }
        public string PlaceTypeName { get; set; }
        public string PlaceTypeNameEN { get; set; }
        public virtual ICollection<Place> Place { get; set; }
    }
}
