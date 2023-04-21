using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Company.Models
{
    public class CompanyDataTable : Datatable
    {
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string[] SortBy { get; set; }

        public bool[] SortDesc { get; set; }

        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}
