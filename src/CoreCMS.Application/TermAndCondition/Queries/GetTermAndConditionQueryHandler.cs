using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.TermAndCondition.Models;
using CoreCMS.Persistence;

namespace CoreCMS.Application.TermAndCondition.Queries
{
    public class GetTermAndConditionQueryHandler : BaseDbContextHandler<GetTermAndConditionQuery, TermAndConditionView>
    {
        public GetTermAndConditionQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<TermAndConditionView> Handle(GetTermAndConditionQuery request, CancellationToken cancellationToken)
        {
            var theme = _context.TermAndCondition.Where(o => !o.IsDeleted && o.TermId == request.TermId)
                .Select(o => new TermAndConditionView()
                {
                    TermId = o.TermId,
                    PlaceId = o.PlaceId,
                    PlaceName = $"{o.Place.PlaceName_TH} ({o.Place.PlaceName_EN})",
                    Name = o.Name,
                    TermTH = o.TermTH,
                    TermEN = o.TermEN,
                    CreateUserId = o.CreatedUserId,
                    CreateUser = o.CreatedUser.DisplayName,
                    CreateDate = o.CreatedDate.Value,
                    InActiveStatus = o.InActiveStatus
                })
                .SingleOrDefault();

            return Task.FromResult(theme);
        }
    }
}