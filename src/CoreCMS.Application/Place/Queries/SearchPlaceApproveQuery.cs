using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;


namespace CoreCMS.Application.Place.Queries
{
    public class SearchPlaceApproveQuery : IRequest<SearchResult<PlaceTableViewModel>>
    {
        public PlaceApproveDataTable DataTable { get; set; }
    }
}
