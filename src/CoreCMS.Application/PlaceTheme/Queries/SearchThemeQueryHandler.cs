using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.PlaceTheme.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CoreCMS.Application.PlaceTheme.Queries
{
    public class SearchThemeQueryHandler : BaseDbContextHandler<SearchThemeQuery, SearchResult<ThemeView>>
    {
        public SearchThemeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SearchResult<ThemeView>> Handle(SearchThemeQuery request, CancellationToken cancellationToken)
        {
            var model = request;

            SearchResult<ThemeView> result = new SearchResult<ThemeView>();
            
            var query = _context.PlaceTheme
                .Where(o => !o.IsDeleted && o.PlaceId == request.PlaceId)
                .Select(o => new ThemeView()
                {
                    ThemeId = o.ThemeId,
                    PlaceId = o.PlaceId,
                    PlaceName = $"{o.Place.PlaceName_TH} ({o.Place.PlaceName_EN})",
                    Name = o.Name,
                    Description = o.Description,
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
                        || o.Description.ToLower().Contains(keyword)
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

                if(col.ToLower() == nameof(ThemeView.CreateDateString).ToLower())
                    col = nameof(ThemeView.CreateDate);

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