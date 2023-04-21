using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Place.Queries
{
    public class Select2PlaceByCompanyQuery : IRequest<OptionViewModel[]>
    {
        public string Search { get; set; }
        public string CompanyId { get; set; }
    }
}
