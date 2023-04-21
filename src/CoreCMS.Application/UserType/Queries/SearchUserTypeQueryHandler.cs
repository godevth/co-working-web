using CoreCMS.Application.UserType.Models;
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

namespace CoreCMS.Application.UserType.Queries
{
    public class SearchUserTypeQueryHandler : BaseDbContextHandler<SearchUserTypeQuery, SearchResult<UserTypeView>>
    {
        public SearchUserTypeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SearchResult<UserTypeView>> Handle(SearchUserTypeQuery request, CancellationToken cancellationToken)
        {
            var model = request.DataTable;
            var query = _context.UserType
            .Select(o => new UserTypeView
            {
                Id = o.UserTypeId,
                Name = o.Name,
                Description = o.Description,
                InActiveStatus = o.InActiveStatus
            });

            SearchResult<UserTypeView> result = new SearchResult<UserTypeView>();

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
            return await Task.FromResult<SearchResult<UserTypeView>>(result);
        }
    }
}
