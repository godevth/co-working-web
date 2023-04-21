using CoreCMS.Application.Facility.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Facility.Queries
{
    public class SearchFacilityQuery : IRequest<SearchResult<FacilityView>>
    {
        public FacilityDataTable DataTable { get; set; }
    }
}
