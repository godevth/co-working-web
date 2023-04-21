using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace CoreCMS.Application.User.Queries
{
    public class SearchUserQueryHandler : BaseDbContextWithMediatorHandler<SearchUserQuery, SearchResult<UserView>>
    {
        public SearchUserQueryHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<SearchResult<UserView>> Handle(SearchUserQuery request, CancellationToken cancellationToken)
        {
            var model = request;

            SearchResult<UserView> result = new SearchResult<UserView>();


            var query = _context.UserSearchView
                .Where(o => !o.IsDeleted)
                .Select(o => new UserView()
                {
                    UserId = o.Id,
                    UserName = o.UserName,
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    DisplayName = o.DisplayName,
                    Position = o.Position,
                    OpenID = o.OpenID,
                    LastLogOnDateTime = o.LastLogOnDateTime,
                    Roles = o.Roles,
                    InActiveStatus = o.InActiveStatus,
                    UserTypeId = o.UserTypeId.HasValue ? o.UserTypeId.Value.ToString() : null,
                    UserType = o.UserType
                });
            

            #region filter SearchKeyword
            if (!string.IsNullOrEmpty(request.SearchKeyword))
            {
                string keyword = request.SearchKeyword.Trim().ToLower();
                query = query.Where(o => 
                        o.UserName.ToLower().Contains(keyword) 
                        || o.FirstName.ToLower().Contains(keyword) 
                        || o.LastName.ToLower().Contains(keyword) 
                        || o.Email.ToLower().Contains(keyword) 
                        || o.PhoneNumber.Contains(keyword)
                        || o.Position.ToLower().Contains(keyword));
            } 
            #endregion

            #region OpenId
            if(model.OpenId.HasValue)
            {
                query = query.Where(o => o.OpenID == model.OpenId.Value);
            }
            #endregion

            #region Role
            if (!string.IsNullOrEmpty(request.Role))
            {
                query = query.Where(o => o.Roles.Contains(request.Role));
            }
            #endregion

            #region UserTypeId
            if (!string.IsNullOrEmpty(request.UserTypeId))
            {
                query = query.Where(o => o.UserTypeId == request.UserTypeId);
            }
            #endregion

            StringBuilder sortBd = new StringBuilder();
            for (int i = 0; i < model.SortBy.Length; i++)
            {
                var col = model.SortBy[i];
                var desc = model.SortDesc[i];

                if(col.ToLower() == nameof(UserView.LastLogOnDateTimeString).ToLower())
                    col = nameof(UserView.LastLogOnDateTime);

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
