using CoreCMS.Application.FacilityType.Models;
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

namespace CoreCMS.Application.FacilityType.Queries
{
    public class SearchFacilityTypeQueryHandler : BaseDbContextHandler<SearchFacilityTypeQuery, SearchResult<FacilityTypeView>>
    {
        public SearchFacilityTypeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SearchResult<FacilityTypeView>> Handle(SearchFacilityTypeQuery request, CancellationToken cancellationToken)
        {
            var model = request.DataTable;
            var query = _context.FacilityType
            .Where(o=>!o.IsDeleted)
            .Select(o => new FacilityTypeView
            {
                Id = o.FacilityTypeID,
                Name_TH =o.FacilityTypeName_TH,
                Name_EN =o.FacilityTypeName_EN,
                InActiveStatus =o.InActiveStatus
            });

            SearchResult<FacilityTypeView> result = new SearchResult<FacilityTypeView>();

            #region filter SearchKeyword
            if (!string.IsNullOrEmpty(model.SearchKeyword))
            {
                var keywords = model.SearchKeyword.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var predicate = query;

                foreach (var kw in keywords)
                {
                    var keyword = kw.Trim();
                        query = query.Where(o =>
                                o.Name_TH.ToLower().Contains(keyword)|| o.Name_EN.ToLower().Contains(keyword));
                }
            }

            if (model.SearchInActiveStatus!=null)
            {
               query= query.Where(x=>x.InActiveStatus==request.DataTable.SearchInActiveStatus);
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
                query = query.OrderBy(sortBd.ToString());
            }

            int skip = (request.DataTable.Page - 1) * request.DataTable.ItemsPerPage;

            result.Total = await query.CountAsync();
            result.Items = await query.Skip(skip).Take(request.DataTable.ItemsPerPage).ToArrayAsync();
            return await Task.FromResult<SearchResult<FacilityTypeView>>(result);
        }
    }
}
