using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Privilege.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.User.Queries;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoreCMS.Application.Privilege.Queries
{
    public class GetPlaceByUserIdQueryHanlder : BaseDbContextWithMediatorHandler<GetPlaceByUserIdQuery, PrivilegePlace>
    {
        public GetPlaceByUserIdQueryHanlder(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<PrivilegePlace> Handle(GetPlaceByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Guid> placeIds = new List<Guid>();
            PrivilegePlace pp = new PrivilegePlace()
            {
                IsPublic = true,
                PlaceIds = placeIds
            };

            GetUserQuery userQuery = new GetUserQuery()
            {
                UserId = request.UserId
            };
            var user = await _mediator.Send(userQuery);
            if(user == null)
            {
                return await Task.FromResult(pp);
            }
            
            var privilegeUsers = _context.Privilege.Where(o => !o.IsDeleted && !o.InActiveStatus 
                && o.PrivilegeTypeCode == "PRIVILEGE_TYPE_PERSON" && o.Place.SeeingTypeCode != "SEEING_TYPE_PUBLIC"
                && o.UserId == request.UserId)
                .Include(o => o.Place)
                .ToList();
            
            var privilegeDomains = _context.Privilege.Where(o => !o.IsDeleted && !o.InActiveStatus 
                && o.PrivilegeTypeCode == "PRIVILEGE_TYPE_DOMAIN" && o.Place.SeeingTypeCode != "SEEING_TYPE_PUBLIC"
                && user.Email.Contains(o.Domain))
                .Include(o => o.Place)
                .ToList();

            var privilege = privilegeUsers.Union(privilegeDomains);

            if(privilege.Any())
            {
                HashSet<Guid> placeIdSet = privilege.Select(o => o.PlaceId).ToHashSet();
                
                #region Check การ Submit Consent ของ User
                if(request.IsCheckConsent)
                {
                    if(placeIdSet.Any())
                    {
                        var userConsents = _context.UserConsentPersistedGrantsView
                            .Where(o => !o.IsDeleted && !o.InActiveStatus && o.UserId == request.UserId
                                && (!o.Expiration.HasValue || (o.Expiration.HasValue && o.Expiration.Value > DateTime.Now)))
                            .ToList();
                        
                        foreach(var pid in placeIdSet)
                        {
                            if(userConsents.Where(o => o.PlaceId == pid).Any())
                                placeIds.Add(pid);
                        }
                    }
                }
                else 
                {
                    placeIds = placeIdSet.ToList();
                }
                #endregion

                bool pv = privilege.Where(o => o.Place.SeeingTypeCode == "SEEING_TYPE_PRIVATE").Any();
                pp.IsPublic = pv;
                pp.PlaceIds = placeIds;
            }

            return await Task.FromResult(pp);
        }
    }
}