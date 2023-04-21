using System;
using System.Collections.Generic;

namespace CoreCMS.Application.Privilege.Models
{
    public class PrivilegePlace
    {
        public PrivilegePlace()
        {
            PlaceIds = new List<Guid>();
        }

        public List<Guid> PlaceIds { get; set; }
        public bool IsPublic { get; set; }
    }
}