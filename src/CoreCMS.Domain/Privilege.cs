using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreCMS.Domain.Base;

namespace CoreCMS.Domain
{
    public class Privilege : ManipulateModel<ApplicationUser>
    {
        [Key]
        public int PrivilegeId { get; set; }

        [StringLength(450)]
        public string Domain { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }

        [ForeignKey ("PrivilegeType")]
        public string PrivilegeTypeCode { get; set; }
        
        [Required]
        public Guid PlaceId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual SystemVariable PrivilegeType { get; set; }
        public virtual Place Place { get; set; }
    }
}