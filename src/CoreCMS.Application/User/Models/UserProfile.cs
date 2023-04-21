using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.User.Models
{
    public class UserProfile
    {

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string PhoneCountryCode { get; set; }
        public string Companyname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string TumbolId { get; set; }

        public string AmphurId { get; set; }

        public string ProvinceId { get; set; }

        public string PostCode { get; set; }
        public string Gender { get; set;}

        public DateTime? BirthDate { get; set; }

        // public string DownloadFileBase64Url { get; set; }
        
        //  public string DownloadFileByteUrl { get; set; }

         public string BirthDateString
        {
            get
            {
                return BirthDate?.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }
    }
}
