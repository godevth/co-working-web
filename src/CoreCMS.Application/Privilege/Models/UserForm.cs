using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Application.Privilege.Models
{
    public class UserForm
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set;}
        
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string PhoneNo { get; set; }

        [Required]
        public string Gender { get; set; }
    }
    public class UserFormNone
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set;}
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
    }
}