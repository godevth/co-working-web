using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Domain
{
    public class UserSearchView
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime? CreateUserDateTime { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public DateTime? LastLogOnDateTime { get; set; }

        public bool OpenID { get; set; }

        public string Position { get; set; }

        public string Roles { get; set; }

         public bool InActiveStatus { get; set; }
        
        public bool IsDeleted { get; set; }

        public int? UserTypeId { get; set; }

        public string UserType { get; set; }
    }
}
