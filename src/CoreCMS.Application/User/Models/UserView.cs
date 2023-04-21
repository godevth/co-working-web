using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.User.Models
{
    public class UserView
    {
        public string UserId { get; set;}

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public bool OpenID { get; set; }

        public string Position { get; set; }

        public DateTime? LastLogOnDateTime { get; set; }

        public string Roles { get; set; }

        public bool InActiveStatus { get; set; }

        public bool IsDeleted { get; set; }
        
        public DateTime? BirthDate { get; set; }

        public string UserTypeId { get; set; }
        public string UserType { get; set; }
        
        public string LastLogOnDateTimeString
        {
            get
            {
                return LastLogOnDateTime?.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }
         public string BirthDateString
        {
            get
            {
                return BirthDate?.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }
    }
}
