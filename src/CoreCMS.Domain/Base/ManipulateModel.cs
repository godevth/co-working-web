using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CoreCMS.Domain.Base
{
    public abstract class ManipulateModel<TUser> : SoftDeleteModel<TUser>
        where TUser : IdentityUser, new () {

            [DefaultValue(false)]
            public bool InActiveStatus { get; set; }

            [Display (Name = "Created By")]
            [ForeignKey ("CreatedUser")]
            public string CreatedUserId { get; set; }

            [Display (Name = "Created Date")]
            public DateTime? CreatedDate { get; set; }

            [Display (Name = "Updated By")]
            [ForeignKey ("UpdatedUser")]
            public string UpdatedUserId { get; set; }

            [Display (Name = "Updated Date")]
            public DateTime? UpdatedDate { get; set; }

            public virtual TUser CreatedUser { get; set; }

            public virtual TUser UpdatedUser { get; set; }
        }
}