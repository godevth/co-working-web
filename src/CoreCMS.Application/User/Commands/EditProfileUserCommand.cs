using CoreCMS.Application.Shared.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Application.User.Commands
{
    public class EditProfileUserCommand : IRequest<CommandResult<string>>
    {
        public string UserId { get; set; }
        public string UpdatedUserId { get; set; }
        public string Email { get; set; }

        [Required]
        public string PhoneCountryCode { get; set; }
        public string Companyname { get; set; }
        [Required]
        [StringLength(450)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(450)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        public int Age { get; set;}

        [Required]
        public string BirthDate { get; set;}
        public DateTime? BirthDateTime { get; set; }

        
        [StringLength(200)]
        public string Address { get; set; }

       
        [StringLength(50)]
        public string TumbolId { get; set; }

        
        [StringLength(50)]
        public string AmphurId { get; set; }

        
        [StringLength(50)]
        public string ProvinceId { get; set; }

        
        [StringLength(50)]
        public string PostCode { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }


        public List<string> Validate() {
            List<string> errors = new List<string>();
            #region Validate BirthDate
            if(!string.IsNullOrEmpty(BirthDate))
            {
                DateTime bDate;
                if (!DateTime.TryParseExact(BirthDate,
                            "yyyy-MM-dd",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None,
                            out bDate))
                {
                    errors.Add("Format ของ BirthDate ไม่ถูกต้อง (ใช้ Format 'YYYY-MM-DD' เท่านั้น)");
                }
                BirthDateTime = bDate;
            }
            #endregion
            #region Validate Gender
            if(!string.IsNullOrEmpty(Gender))
            {
                if(!(Gender.ToUpper() == "F" || Gender.ToUpper() == "M"))
                {
                    errors.Add("ค่าของ Gender ไม่ถูกต้อง (ใช้ 'F' or 'M' เท่านั้น)");
                }
            }
            #endregion

            return errors;
        }
    }
}