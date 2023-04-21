using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreCMS.Domain
{
    public class ApplicationRole : IdentityRole
    {
        [StringLength(450)]
        public string Description { get; set; }
        public int? UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
