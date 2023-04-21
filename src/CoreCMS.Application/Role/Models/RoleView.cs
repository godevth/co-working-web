using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Role.Models
{
    public class RoleView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? UserTypeId { get; set; }
        public string UserTypeName { get; set; }
    }
}
