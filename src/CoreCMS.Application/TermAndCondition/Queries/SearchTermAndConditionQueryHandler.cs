using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.TermAndCondition.Models;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CoreCMS.Application.TermAndCondition.Queries
{
    public class SearchTermAndConditionQueryHandler : BaseDbContextHandler<SearchTermAndConditionQuery, SearchResult<TermAndConditionView>>
    {
        public SearchTermAndConditionQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SearchResult<TermAndConditionView>> Handle(SearchTermAndConditionQuery request, CancellationToken cancellationToken)
        {
            var model = request;

            SearchResult<TermAndConditionView> result = new SearchResult<TermAndConditionView>();
            
            var query = _context.TermAndCondition
                .Where(o => !o.IsDeleted && o.PlaceId == request.PlaceId)
                .Select(o => new TermAndConditionView()
                {
                    TermId = o.TermId,
                    PlaceId = o.PlaceId,
                    PlaceName = $"{o.Place.PlaceName_TH} ({o.Place.PlaceName_EN})",
                    Name = o.Name,
                    CreateUserId = o.CreatedUserId,
                    CreateUser = o.CreatedUser.DisplayName,
                    CreateDate = o.CreatedDate.Value,
                    InActiveStatus = o.InActiveStatus
                });

            #region filter SearchKeyword
            if (!string.IsNullOrEmpty(request.SearchKeyword))
            {
                string keyword = request.SearchKeyword.Trim().ToLower();
                query = query.Where(o =>
                        o.Name.ToLower().Contains(keyword)
                        || o.CreateUser.ToLower().Contains(keyword));
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

                if(col.ToLower() == nameof(TermAndConditionView.CreateDateString).ToLower())
                    col = nameof(TermAndConditionView.CreateDate);

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