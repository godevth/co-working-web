﻿using CoreCMS.Application.Facility.Models;
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

namespace CoreCMS.Application.Facility.Queries
{
    public class SearchFacilityQueryHandler : BaseDbContextHandler<SearchFacilityQuery, SearchResult<FacilityView>>
    {
        public SearchFacilityQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SearchResult<FacilityView>> Handle(SearchFacilityQuery request, CancellationToken cancellationToken)
        {
            var model = request.DataTable;
            var query = _context.Facility
            .Where(o=>!o.IsDeleted)
            .Select(o => new FacilityView
            {
                Id = o.FacilityId,
                Name_TH =o.FacilityName_TH,
                Name_EN =o.FacilityName_EN,
                FacilityTypeId = o.FacilityTypeID.ToString(),
                FacilityTypeName =o.FacilityType.FacilityTypeName_TH,
                InActiveStatus =o.InActiveStatus
            });

            SearchResult<FacilityView> result = new SearchResult<FacilityView>();

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

            if (!string.IsNullOrEmpty(model.FacilityTypeId))
            {
               query= query.Where(x=>x.FacilityTypeId==request.DataTable.FacilityTypeId);
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
            return await Task.FromResult<SearchResult<FacilityView>>(result);
        }
    }
}
