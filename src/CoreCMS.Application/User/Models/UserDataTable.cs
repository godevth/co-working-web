using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.User.Models
{
    public class UserDataTable : Datatable
    {
        public bool? OpenId { get; set; }
    }
}
