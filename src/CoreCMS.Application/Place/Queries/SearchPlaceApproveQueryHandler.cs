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
    public class SearchPlaceApproveQueryHandler : BaseDbContextHandler<SearchPlaceApproveQuery, SearchResult<PlaceTableViewModel>>
    {
        private readonly IMediator _mediator;
        public SearchPlaceApproveQueryHandler(ApplicationDbContext context, IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }

        public override async Task<SearchResult<PlaceTableViewModel>> Handle(SearchPlaceApproveQuery request, CancellationToken cancellationToken)
        {
            SearchResult<PlaceTableViewModel> result = new SearchResult<PlaceTableViewModel>();
                var model = request.DataTable;
                var query = _context.Place
                .Where(o => (!o.IsApproveCreate || o.IsApproveDelete) && !o.IsDeleted);

              

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

                if(model.ApproveStatus.HasValue)
                {
                    if( model.ApproveStatus.Value == false)
                    {
                        query = query.Where(o => !o.IsApproveDelete);
                    }
                    if( model.ApproveStatus.Value == true)
                    {
                        query = query.Where(o =>  o.IsApproveDelete);
                    }
                }
                #endregion




                var resultQuery = query
                .Select(o => new PlaceTableViewModel
                {
                    Id = o.PlaceId.ToString(),
                    PlaceNameTH = o.PlaceName_TH,
                    PlaceNameEN = o.PlaceName_EN,
                    InActiveStatus = o.InActiveStatus,
                    ProvinceId = o.PROVINCE_ID.Value,
                    AmpherId = o.AMPER_ID.Value,
                    Address = o.Address,
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
                return await Task.FromResult<SearchResult<PlaceTableViewModel>>(result);
        
        }
    }
}
