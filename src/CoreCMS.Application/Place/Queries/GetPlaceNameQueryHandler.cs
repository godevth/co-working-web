using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Persistence;

namespace CoreCMS.Application.Place.Queries
{
    public class GetPlaceNameQueryHandler : BaseDbContextHandler<GetPlaceNameQuery, PlaceModels>
    {
        public GetPlaceNameQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<PlaceModels> Handle(GetPlaceNameQuery request, CancellationToken cancellationToken)
        {
            var place = _context.Place.Where(o => !o.IsDeleted && o.PlaceId == request.Id)
                .Select(o => new PlaceModels()
                {
                    PlaceId = o.PlaceId,
                    Name_TH = o.PlaceName_TH,
                    Name_EN = o.PlaceName_EN 
                })
                .SingleOrDefault();

            return Task.FromResult(place);
        }
    }
}