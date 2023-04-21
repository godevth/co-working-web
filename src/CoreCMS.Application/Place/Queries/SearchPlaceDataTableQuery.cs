using CoreCMS.Application.Place.Models;
using CoreCMS.Application.PlaceType.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Place.Queries
{
    public class SearchPlaceDataTableQuery : IRequest<SearchResult<PlaceTableViewModel>>
    {
        public PlaceDataTable DataTable { get; set; }
    }
}
