using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreCMS.Domain
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(450)]
        public string GivenName { get; set; }

        [StringLength(450)]
        public string FamilyName { get; set; }

        [StringLength(450)]
        public string FullName { get; set; }

        [StringLength(450)]
        public string Position { get; set; }

        [StringLength(450)]
        public string DisplayName { get; set; }

        public bool OpenID { get; set; }

        [StringLength(450)]
        public string FirstName { get; set; }

        [StringLength(450)]
        public string LastName { get; set; }

        [StringLength(450)]
        public string EmployeeCode { get; set; }

        public DateTime? CreateUserDateTime { get; set; }

        public DateTime? LastLogOnDateTime { get; set; }

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

        public string PhoneCountryCode { get; set; }
        public string Companyname { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public DateTime? BirthDate { get; set;}

        #region ManipulateModel
        [DefaultValue(false)]
        public bool InActiveStatus { get; set; }

        public string CreatedUserId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdatedUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public string DeletedUserId { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string RandomCode { get; set; }
        public DateTime? ConfirmEmailDate { get; set; }
        #endregion
    }
}
