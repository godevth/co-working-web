using CoreCMS.Application.Room.Models;
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

namespace CoreCMS.Application.Room.Queries
{
    public class SearchRoomQueryHandler : BaseDbContextHandler<SearchRoomQuery, SearchResult<RoomView>>
    {
        public SearchRoomQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SearchResult<RoomView>> Handle(SearchRoomQuery request, CancellationToken cancellationToken)
        {

            var model = request.DataTable;
            var query = _context.Room
            .Where(o=>!o.IsDeleted && o.PlaceId==request.DataTable.Id);


                 if (!string.IsNullOrEmpty(model.RoomTypeId))
                    {
                        query=query.Where(x=>x.RoomTypeId==Convert.ToInt32(model.RoomTypeId));
                    }
                    if (model.FacilityId != null)
                    {
                        query = query.Where(x=>x.FacilityServices.Any(o=>o.FacilityId==new Guid(model.FacilityId)));
                    }

                       var resultQuery =query.Select(o => new RoomView
            {
                Id = o.RoomId,
                Name_TH =o.RoomName_TH,
                Name_EN = o.RoomName_EN,
                RoomTypeId = o.RoomTypeId,
                RoomTypeName =o.RoomType.RoomTypeName,
                PlaceId = o.PlaceId,
                PlaceName = $"{o.Place.PlaceName_TH} ({o.Place.PlaceName_EN})",
                // Qty =o.RoomQty,
                // Price =o.Price,
                Capacity =o.Capacity,
                FacilityList = _context.FacilityServices.Where(r=>r.RoomId==o.RoomId).Select(r=>r.Facility.FacilityName_TH).ToList(),
                InActiveStatus =o.InActiveStatus
            });

            SearchResult<RoomView> result = new SearchResult<RoomView>();

            #region filter SearchKeyword
            if (!string.IsNullOrEmpty(model.SearchKeyword))
            {
                var keywords = model.SearchKeyword.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var predicate = query;

                foreach (var kw in keywords)
                {
                    var keyword = kw.Trim();
                        resultQuery = resultQuery.Where(o =>
                                o.Name_TH.ToLower().Contains(keyword)||o.Name_EN.ToLower().Contains(keyword));
                }
            }
            if (model.SearchInActiveStatus!=null)
            {
               resultQuery= resultQuery.Where(x=>x.InActiveStatus==request.DataTable.SearchInActiveStatus);
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
            foreach (var item in result.Items)
                    {
                        var list = item.FacilityList.Distinct();
                        item.Facility = String.Join(",", list);
                    }
            return await Task.FromResult<SearchResult<RoomView>>(result);
        }
    }
}
