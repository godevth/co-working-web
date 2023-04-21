using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class IsRoleOwnerAdminPlaceCommandHandler : BaseDbContextWithMediatorHandler<IsRoleOwnerAdminPlaceCommand, bool>
    {
        public IsRoleOwnerAdminPlaceCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override Task<bool> Handle(IsRoleOwnerAdminPlaceCommand request, CancellationToken cancellationToken)
        {
            bool hasRole = request.IsSuperAdmin;
            bool chk = hasRole;

            #region Check Roles Owner Place
            if(!chk)
            {
                var places = _context.CompanyProfiles
                    .Where(o => !o.IsDeleted && !o.InActiveStatus && o.Company.OwnerId == request.UserId)
                    .Select(o => o.PlaceId)
                    .ToList();
                if(places.Any())
                {
                    if(!places.Where(o => o == request.PlaceId).Any())
                        chk = false;
                    else
                        chk = true;
                }
            }
            #endregion

            #region Check Roles Admin Place
            if(!chk)
            {
                var places = _context.CompanyProfiles
                    .Where(o => !o.IsDeleted && !o.InActiveStatus && o.AdminId == request.UserId)
                    .Select(o => o.PlaceId)
                    .ToList();
                if(places.Any())
                {
                    if(!places.Where(o => o == request.PlaceId).Any())
                        chk = false;
                    else
                        chk = true;
                }
            }
            #endregion
            
            return Task.FromResult(chk);
        }
    }
}