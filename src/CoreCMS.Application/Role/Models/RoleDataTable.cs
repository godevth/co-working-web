using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Role.Models
{
    public class RoleDataTable : Datatable
    {
        public int? UserTypeId { get; set; }
        public string[] SortBy { get; set; }
        public bool[] SortDesc { get; set; }
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}
