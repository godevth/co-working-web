using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Privilege.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.TermAndCondition.Models;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Queries
{
    public class GetPlaceConsentByUserIdQueryHandler : BaseDbContextWithMediatorHandler<GetPlaceConsentByUserIdQuery, SearchMobileResult<PlaceConsentView>>
    {
        public GetPlaceConsentByUserIdQueryHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<SearchMobileResult<PlaceConsentView>> Handle(GetPlaceConsentByUserIdQuery request, CancellationToken cancellationToken)
        {
            request.ItemsPerPage = request.ItemsPerPage > 0 ? request.ItemsPerPage : 20;
            request.Page = request.Page > 0 ? request.Page : 1;
            int page = request.Page - 1;

            var query = _context.TermAndCondition.Where(o => !o.IsDeleted && !o.InActiveStatus);

            #region Get SSO Consent 
            GetConsentCommand consentCommand = new GetConsentCommand()
            {
                UserId = !string.IsNullOrEmpty(request.UserId) ? request.UserId : null
            };
            var consent = await _mediator.Send(consentCommand);
            #endregion

            if(string.IsNullOrEmpty(request.UserId))
            {
                var tmps = new SearchMobileResult<PlaceConsentView>();
                if(consent.Status)
                {
                    tmps.Total = 1;
                    tmps.Items = new PlaceConsentView[] {
                        new PlaceConsentView()
                        {
                            IsDefault = true,
                            TermId = 0,
                            Name = "Default",
                            TermAndCondition = request.Language == "EN" ? consent.Data.Result.TermsAndConEN : consent.Data.Result.TermsAndConTH
                        }
                    };
                }
                return await Task.FromResult(tmps);
            }

            #region Get Place ตามสิทธิ์ของ User
            GetPlaceByUserIdQuery getPlaceByUser = new GetPlaceByUserIdQuery()
            {
                UserId = request.UserId,
                IsCheckConsent = false
            };
            var placeByUser = await _mediator.Send(getPlaceByUser);
            
            //user ยังไม่ submit consent
            List<Guid> placeIds = new List<Guid>();
            if(placeByUser.PlaceIds.Any())
            {
                var userConsents = _context.UserConsentPersistedGrantsView
                    .Where(o => !o.IsDeleted && !o.InActiveStatus && o.UserId == request.UserId
                        && (!o.Expiration.HasValue || (o.Expiration.HasValue && o.Expiration.Value > DateTime.Now)))
                    .ToList();
                
                foreach(var pid in placeByUser.PlaceIds)
                {
                    if(!userConsents.Where(o => o.PlaceId == pid).Any())
                        placeIds.Add(pid);
                }
                query = query.Where(o => placeIds.Contains(o.PlaceId));
            }
            #endregion

            var count = query.Count();
            var sQuery = query.Select(o => new PlaceConsentView()
                    {
                        Name = o.Name,
                        PlaceId = o.PlaceId,
                        PlaceName = request.Language == "EN" ? o.Place.PlaceName_EN : o.Place.PlaceName_TH,
                        TermId = o.TermId,
                        TermAndCondition = request.Language == "EN" ? o.TermEN : o.TermTH
                    });

            #region Union Default TermAndCondition (SSO)
            IEnumerable<PlaceConsentView> unions = sQuery.AsQueryable();
            if(consent.Status && !consent.Data.Result.AnotherProviderGranted)
            {
                count++;
                unions = unions.Union(new PlaceConsentView[] {
                    new PlaceConsentView()
                    {
                        IsDefault = true,
                        TermId = 0,
                        Name = "Default",
                        TermAndCondition = request.Language == "EN" ? consent.Data.Result.TermsAndConEN : consent.Data.Result.TermsAndConTH
                    }
                });
            }
            #endregion

            var items = unions.Skip(page * request.ItemsPerPage).Take(request.ItemsPerPage).OrderBy(o => o.TermId).ToArray();
            var result = new SearchMobileResult<PlaceConsentView>(){
                More = (request.Page * request.ItemsPerPage) < count,
                Total = count,
                Items = items
            };

            return await Task.FromResult(result);
        }
    }
}