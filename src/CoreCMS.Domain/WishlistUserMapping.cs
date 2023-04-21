using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
    public class WishlistUserMapping : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }
        public bool IsWishlist { get; set; }

         public virtual Place Place { get; set; }
         public virtual ApplicationUser ApplicationUser { get; set; }
        
    }
}