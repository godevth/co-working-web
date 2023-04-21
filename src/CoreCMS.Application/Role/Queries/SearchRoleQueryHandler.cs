using CoreCMS.Application.Facility.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using CoreCMS.Application.Role.Models;

namespace CoreCMS.Application.Role.Queries
{
    public class SearchRoleQueryHandler : BaseDbContextHandler<SearchRoleQuery, SearchResult<RoleView>>
    {
        public SearchRoleQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SearchResult<RoleView>> Handle(SearchRoleQuery request, CancellationToken cancellationToken)
        {
            var model = request.DataTable;
            var query = _context.Roles
            .Select(o => new RoleView
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description,
                UserTypeId = o.UserTypeId,
                UserTypeName = o.UserType.Description
            });

            SearchResult<RoleView> result = new SearchResult<RoleView>();

            #region filter SearchKeyword
            if (!string.IsNullOrEmpty(model.SearchKeyword))
            {
                var keywords = model.SearchKeyword.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var predicate = query;

                foreach (var kw in keywords)
                {
                    var keyword = kw.Trim();
                    query = query.Where(o =>
                            o.Name.ToLower().Contains(keyword) || o.Description.ToLower().Contains(keyword));
                }
            }

            if (model.UserTypeId.HasValue)
            {
                query = query.Where(x => x.UserTypeId.Value == model.UserTypeId);
            }
            //if (model.SearchInActiveStatus != null)
            //{
            //    query = query.Where(x => x.InActiveStatus == request.DataTable.SearchInActiveStatus);
            //}
            #endregion
            StringBuilder sortBd = new StringBuilder();
            for (int i = 0; i < request.DataTable.SortBy.Length; i++)
            {
                var col = request.DataTable.SortBy[i];
                var desc = request.DataTable.SortDesc[i];

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

            int skip = (request.DataTable.Page - 1) * request.DataTable.ItemsPerPage;
            result.Total = await query.CountAsync();
            result.Items = await query.Skip(skip).Take(request.DataTable.ItemsPerPage).ToArrayAsync();
            return await Task.FromResult<SearchResult<RoleView>>(result);
        }
    }
}
