using System;

namespace CoreCMS.Application.Privilege.Models
{
    public class PrivilegeView
    {
        public int PrivilegeId { get; set; }
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PrivilegeTypeCode { get; set; }
        public string PrivilegeTypeName { get; set; }
        public string Domain { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool InActiveStatus { get; set; }
    }
}