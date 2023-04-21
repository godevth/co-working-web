using CoreCMS.Application.Company.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Company.Queries
{
    public class Select2CompanyQuery : IRequest<OptionViewModel[]>
    {
        public string Search { get; set; }
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
