using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoreCMS.Application.User.Queries
{
    public class Select2UserPrivilegeQueryHandler : BaseDbContextHandler<Select2UserPrivilegeQuery, OptionViewModel[]>
    {
        public Select2UserPrivilegeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2UserPrivilegeQuery request, CancellationToken cancellationToken)
        {
            var ignoreUsers = _context.Privilege
                .Where(o => !o.IsDeleted && !o.InActiveStatus && o.PlaceId == request.PlaceId && o.PrivilegeTypeCode == "PRIVILEGE_TYPE_PERSON")
                .Select(o => o.UserId)
                .ToList();

            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;

            var query = _context.Users
                .Where(o => !o.IsDeleted && !o.InActiveStatus 
                    && ignoreUsers.Any() ? !ignoreUsers.Contains(o.Id) : true 
                    && o.DisplayName.ToUpper().Contains(search))
                .Select(o => new OptionViewModel
                {
                    Text = $"[{o.Email}] {o.DisplayName}",
                    Value = o.Id.ToString()
                });

            return await query.ToArrayAsync();
        }
    }
}