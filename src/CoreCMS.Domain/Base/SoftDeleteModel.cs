using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CoreCMS.Domain.Base
{
       public abstract class SoftDeleteModel<TUser>
        where TUser : IdentityUser, new () {

            [DefaultValue(false)]
            public bool IsDeleted { get; set; }

            [ForeignKey ("DeletedUser")]
            public string DeletedUserId { get; set; }

            public DateTime? DeletedDate { get; set; }

            public virtual TUser DeletedUser { get; set; }
        }
}