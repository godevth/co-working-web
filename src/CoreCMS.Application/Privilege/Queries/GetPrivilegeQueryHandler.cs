using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Privilege.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Persistence;

namespace CoreCMS.Application.Privilege.Queries
{
    public class GetPrivilegeQueryHandler : BaseDbContextHandler<GetPrivilegeQuery, PrivilegeView>
    {
        public GetPrivilegeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<PrivilegeView> Handle(GetPrivilegeQuery request, CancellationToken cancellationToken)
        {
            var privilege = _context.Privilege.Where(o => !o.IsDeleted && o.PrivilegeId == request.PrivilegeId)
                .Select(o => new PrivilegeView()
                {
                    PrivilegeId = o.PrivilegeId,
                    PlaceId = o.PlaceId,
                    PlaceName = $"{o.Place.PlaceName_TH} ({o.Place.PlaceName_EN})" ,
                    PrivilegeTypeCode = o.PrivilegeTypeCode,
                    PrivilegeTypeName = o.PrivilegeType.SystemVariableName,
                    Domain = o.Domain,
                    UserId = o.UserId,
                    Email = !string.IsNullOrEmpty(o.UserId) ? o.User.Email : null,
                    FirstName = !string.IsNullOrEmpty(o.UserId) ? o.User.FirstName : null,
                    LastName = !string.IsNullOrEmpty(o.UserId) ? o.User.LastName : null,
                    DisplayName = !string.IsNullOrEmpty(o.UserId) ? o.User.DisplayName : null,
                    PhoneNumber = !string.IsNullOrEmpty(o.UserId) ? o.User.PhoneNumber : null,
                    InActiveStatus = o.InActiveStatus
                })
                .SingleOrDefault();

            return Task.FromResult(privilege);
        }
    }
}