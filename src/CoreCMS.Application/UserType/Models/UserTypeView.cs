using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.UserType.Models
{
    public class UserTypeView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool InActiveStatus { get; set; }
    }
}
