using CoreCMS.Application.FacilityAndPlaceType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.FacilityAndPlaceType.Queries
{
    public class GetFacilityAndPlaceTypeQuery : IRequest<SearchMobileResult<FacilityAndPlaceTypeViewModel>>
    {
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }
        public string Language { get; set; }
        public string SearchKeyword { get; set; }
    }
}
