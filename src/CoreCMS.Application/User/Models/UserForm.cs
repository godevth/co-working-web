using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Application.User.Models
{
    public class UserForm
    {
        [Required]
        [StringLength(256)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20)]
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(450)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(450)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Position { get; set; }

        public string Role { get; set; }

        public bool InActiveStatus { get; set; }

        public string BirthDateString { get; set;}

        public DateTime BirthDate { get; set;}

        public string UserTypeId { get; set; }

        public List<string> Validate() {
            List<string> errors = new List<string>();

            #region Parse BirthDate
            DateTime bdDate;
            string st = string.Format("{0}", BirthDateString);
            if (!DateTime.TryParseExact(st,
                        "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out bdDate))
            {
                errors.Add("Format ของ BirthDate ไม่ถูกต้อง");
            }
            BirthDate = bdDate;
            #endregion


            return errors;
        }
    }
}