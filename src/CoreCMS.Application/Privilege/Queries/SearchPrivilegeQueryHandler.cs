using CoreCMS.Application.Privilege.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace CoreCMS.Application.Privilege.Queries
{
    public class SearchPrivilegeQueryHandler : BaseDbContextHandler<SearchPrivilegeQuery, SearchResult<PrivilegeView>>
    {
        public SearchPrivilegeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SearchResult<PrivilegeView>> Handle(SearchPrivilegeQuery request, CancellationToken cancellationToken)
        {
            var model = request;

            SearchResult<PrivilegeView> result = new SearchResult<PrivilegeView>();
            
            var query = _context.Privilege
                .Where(o => !o.IsDeleted && o.PlaceId == request.PlaceId)
                .Select(o => new PrivilegeView()
                {
                    PrivilegeId = o.PrivilegeId,
                    PlaceId = o.PlaceId,
                    PlaceName = $"{o.Place.PlaceName_TH} ({o.Place.PlaceName_EN})",
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
                });

            #region filter SearchKeyword
            if (!string.IsNullOrEmpty(request.SearchKeyword))
            {
                string keyword = request.SearchKeyword.Trim().ToLower();
                query = query.Where(o =>
                        o.Domain.ToLower().Contains(keyword)
                        || o.FirstName.ToLower().Contains(keyword)
                        || o.LastName.ToLower().Contains(keyword)
                        || o.Email.ToLower().Contains(keyword)
                        || o.PhoneNumber.Contains(keyword));
            }
            #endregion

            #region PrivilegeTypeCode
            if (!string.IsNullOrEmpty(request.PrivilegeTypeCode))
            {
                query = query.Where(o => o.PrivilegeTypeCode == request.PrivilegeTypeCode);
            }
            #endregion

            #region InActiveStatus
            if(request.InActiveStatus.HasValue)
            {
                query = query.Where(o => o.InActiveStatus == request.InActiveStatus.Value);
            }
            #endregion

            StringBuilder sortBd = new StringBuilder();
            for (int i = 0; i < model.SortBy.Length; i++)
            {
                var col = model.SortBy[i];
                var desc = model.SortDesc[i];

                if (desc)
                {
                    sortBd.Append($"{col} DESC,");
                }
                else
                {
                    sortBd.Append($"{col},");
                }
            }

            if (sortBd.Length > 0)
            {
                sortBd.Length--;
                query = query.OrderBy(sortBd.ToString());
            }

            int skip = (model.Page - 1) * model.ItemsPerPage;

            result.Total = await query.CountAsync();
            result.Items = await query.Skip(skip).Take(model.ItemsPerPage).ToArrayAsync();

            return result;
        }
    }
}
