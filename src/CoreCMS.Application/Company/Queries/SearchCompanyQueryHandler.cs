using CoreCMS.Application.Company.Models;
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

namespace CoreCMS.Application.Company.Queries
{
    public class SearchCompanyQueryHandler : BaseDbContextHandler<SearchCompanyQuery, SearchResult<CompanyView>>
    {
        public SearchCompanyQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SearchResult<CompanyView>> Handle(SearchCompanyQuery request, CancellationToken cancellationToken)
        {

            var model = request.DataTable;
            var query = _context.Company.Where(o => !o.IsDeleted);
            if(!request.DataTable.IsAdmin){
                query = query.Where(o=>o.OwnerId==request.DataTable.UserId);
            }
            var resultQuery = query.Select(o => new CompanyView
            {
                Id = o.CompanyId,
                Name_TH = o.CompanyName_TH,
                Name_EN = o.CompanyName_EN,
                Owner = o.Owner.DisplayName,
                InActiveStatus = o.InActiveStatus
            });

            SearchResult<CompanyView> result = new SearchResult<CompanyView>();

            #region filter SearchKeyword
            if (!string.IsNullOrEmpty(model.SearchKeyword))
            {
                var keywords = model.SearchKeyword.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var predicate = query;

                foreach (var kw in keywords)
                {
                    var keyword = kw.Trim();
                    resultQuery = resultQuery.Where(o =>
                            o.Name_TH.ToLower().Contains(keyword) || o.Name_EN.ToLower().Contains(keyword));
                }
            }
            if (model.SearchInActiveStatus != null)
            {
                resultQuery = resultQuery.Where(x => x.InActiveStatus == request.DataTable.SearchInActiveStatus);
            }
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
                resultQuery = resultQuery.OrderBy(sortBd.ToString());
            }

            int skip = (request.DataTable.Page - 1) * request.DataTable.ItemsPerPage;

            result.Total = await resultQuery.CountAsync();
            result.Items = await resultQuery.Skip(skip).Take(request.DataTable.ItemsPerPage).ToArrayAsync();
            return await Task.FromResult<SearchResult<CompanyView>>(result);
        }
    }
}
