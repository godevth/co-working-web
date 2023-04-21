using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ImplementationDate.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Persistence;

namespace CoreCMS.Application.ImplementationDate.Queries
{
    public class SearchImplementationDateQueryHandler : BaseDbContextHandler<SearchImplementationDateQuery, List<ImplementationDateView>>
    {
        public SearchImplementationDateQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<List<ImplementationDateView>> Handle(SearchImplementationDateQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ImplementationDate.Where(o => !o.IsDeleted && !o.InActiveStatus && o.PlaceId == request.PlaceId);
            
            if(request.StartDays.Any())
            {
                query = query.Where(o => request.StartDays.Contains(o.StartDay));
            }

            var implementationDateViews = query.Select(o => new ImplementationDateView()
                    {
                        PlaceId = o.PlaceId,
                        ImplementationDateId = o.ImplementationDateId,
                        StartDay = o.StartDay,
                        StartTime = o.StartTime, 
                        EndTime = o.EndTime
                    })
                    .ToList();
            
            return Task.FromResult(implementationDateViews);
        }
    }
}