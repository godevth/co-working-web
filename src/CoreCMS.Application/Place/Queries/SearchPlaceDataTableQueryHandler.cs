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
using CoreCMS.Application.Place.Models;
using CoreCMS.Application.ApiMasterData.Queries;
using MediatR;

namespace CoreCMS.Application.Place.Queries
{
    public class SearchPlaceDataQueryHandler : BaseDbContextHandler<SearchPlaceDataTableQuery, SearchResult<PlaceTableViewModel>>
    {
        private readonly IMediator _mediator;
        public SearchPlaceDataQueryHandler(ApplicationDbContext context, IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }

        public override async Task<SearchResult<PlaceTableViewModel>> Handle(SearchPlaceDataTableQuery request, CancellationToken cancellationToken)
        {
            SearchResult<PlaceTableViewModel> result = new SearchResult<PlaceTableViewModel>();
                var model = request.DataTable;
                var query = _context.Place
                .Where(o => !o.IsDeleted);

                if(!request.DataTable.IsAdmin){
                query = query.Where(o=>o.Company.OwnerId==request.DataTable.UserId || o.Company.CompanyProfiles.Any(c=>c.AdminId==request.DataTable.UserId));
            }

                #region filter SearchKeyword
                if (!string.IsNullOrEmpty(model.SearchKeyword))
                {
                    var keywords = model.SearchKeyword.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var predicate = query;

                    foreach (var kw in keywords)
                    {
                        var keyword = kw.Trim();
                        query = query.Where(o =>
                                o.PlaceName_TH.ToLower().Contains(keyword)||
                                o.PlaceName_EN.ToLower().Contains(keyword)
                                );
                    }
                }
                var provinces = await _mediator.Send(new SearchMasterProvinceQuery() { });
                var amphers = await _mediator.Send(new SearchMasterAmpherQuery() { });
                if (model.AmpherId != null)
                {
                    query = query.Where(x => x.AMPER_ID == Convert.ToInt32(model.AmpherId));
                }
                else if (model.ProvinceId != null)
                {
                    query = query.Where(x => x.PROVINCE_ID == Convert.ToInt32(model.ProvinceId));
                }

                if (!String.IsNullOrEmpty(model.PlaceTypeId))
                {
                    query = query.Where(x => x.PlaceTypeID == Convert.ToInt32(model.PlaceTypeId));
                }

                if (model.SearchInActiveStatus != null)
                {

                    query = query.Where(x => x.InActiveStatus == model.SearchInActiveStatus);
                }

                 if (!string.IsNullOrEmpty(model.RoomTypeId))
                    {
                        query=query.Where(x=>x.Room.Any(q=>q.RoomTypeId==Convert.ToInt32(model.RoomTypeId)));
                    }
                    if (model.FacilityId != null)
                    {
                        query = query.Where(x=>x.FacilityServices.Any(o=>o.FacilityId==new Guid(model.FacilityId)));
                    }

                #endregion




                var resultQuery = query
                .Select(o => new PlaceTableViewModel
                {
                    Id = o.PlaceId.ToString(),
                    PlaceNameTH = o.PlaceName_TH,
                    PlaceNameEN = o.PlaceName_EN,
                    PlaceTypeName = o.PlaceType.PlaceTypeName,
                    RoomTypeList = o.Room.Where(r=>r.PlaceId==o.PlaceId&&!r.IsDeleted).Select(r => r.RoomType.RoomTypeName).ToList(),
                    FacilityList = o.FacilityServices.Where(fs=>fs.PlaceId==o.PlaceId&&!fs.IsDeleted).Select(f => f.Facility.FacilityName_TH).ToList(),
                    InActiveStatus = o.InActiveStatus,
                    ProvinceId = o.PROVINCE_ID.Value,
                    AmpherId = o.AMPER_ID.Value,
                    Address = o.Address,
                    SeeingCode = o.SeeingTypeCode,
                    IsApproveCreate = o.IsApproveCreate,
                    IsApproveDelete = o.IsApproveDelete
                });

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
                        item.ProvinceNameTH = provinces != null && item.ProvinceId != 0 ?
                            provinces.Data.Where(p => p.ProvinceId == item.ProvinceId.ToString()).Select(p => p.ProvinceTH).FirstOrDefault() : null;

                        item.AmpherNameTH = amphers != null && item.AmpherId != 0 ?
                            amphers.Data.Where(p => p.AmpherId == item.AmpherId.ToString()).Select(p => p.AmpherTH).FirstOrDefault() : null;

                        // item.TambolName = tambols != null && !string.IsNullOrEmpty(item.TambolId) ?
                        //     tambols.Where(p => p.TumbolId == item.TambolId).Select(p => p.TumbolTH).FirstOrDefault() : null;
                        var listRoom = item.RoomTypeList.Distinct();
                        var listFacility = item.FacilityList.Distinct();
                        item.RoomType = String.Join(",", listRoom);
                        item.Facility = String.Join(",", listFacility);
                    }
                return await Task.FromResult<SearchResult<PlaceTableViewModel>>(result);
        
        }
    }
}
