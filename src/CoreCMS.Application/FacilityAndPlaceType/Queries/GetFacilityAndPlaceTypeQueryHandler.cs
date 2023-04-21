using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Place.Models;
using System.Collections.Generic;
using System.Linq;
using CoreCMS.Application.FacilityAndPlaceType.Queries;
using CoreCMS.Application.FacilityAndPlaceType.Models;

namespace CoreCMS.Application.FacilityAndPlaceType.Queries
{
    public class GetFacilityAndPlaceTypeQueryHandler : BaseDbContextHandler<GetFacilityAndPlaceTypeQuery,  SearchMobileResult<FacilityAndPlaceTypeViewModel>>
    {
        public GetFacilityAndPlaceTypeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SearchMobileResult<FacilityAndPlaceTypeViewModel>> Handle(GetFacilityAndPlaceTypeQuery request, CancellationToken cancellationToken)
        {

            var facility = _context.Facility.Where(o => !o.IsDeleted).ToList();
            var roomType = _context.RoomType.Where(o => !o.IsDeleted).ToList();
            SearchMobileResult<FacilityAndPlaceTypeViewModel> result = new SearchMobileResult<FacilityAndPlaceTypeViewModel>()
            {
                More = false
            };

            var language = request.Language;
            List<FacilityAndPlaceTypeViewModel> facilityAndPlace = new List<FacilityAndPlaceTypeViewModel>();
            foreach(var item in facility)
            {
                var items = new FacilityAndPlaceTypeViewModel
                {
                    Id = item.FacilityId.ToString(),
                    Name = language == "EN" ? item.FacilityName_EN : item.FacilityName_TH,
                    Type = "Facility"
                };
                facilityAndPlace.Add(items);
            }
            foreach(var item in roomType)
            {
                var items = new FacilityAndPlaceTypeViewModel
                {
                    Id = item.RoomTypeID.ToString(),
                    Name = language == "EN" ? item.RoomTypeName : item.RoomTypeName,
                    Type = "RoomType"
                };
                facilityAndPlace.Add(items);
            }

            result.Total = facilityAndPlace.Count();
            result.Items = facilityAndPlace.ToArray();
            if (!string.IsNullOrEmpty(request.SearchKeyword))
            {
                result.Items = result.Items.Where(x => x.Name.Contains(request.SearchKeyword)).ToArray();
            }
            
            return await Task.FromResult<SearchMobileResult<FacilityAndPlaceTypeViewModel>>(result);
        }
    }
}
