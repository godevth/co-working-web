using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Application.Privilege.Models
{
    public class PrivilegeForm
    {
        public PrivilegeForm()
        {
            Users = new List<string>();
        }
        
        [Required]
        public Guid PlaceId { get; set; }

        [Required]
        public List<string> Users { get; set; }
    }
}