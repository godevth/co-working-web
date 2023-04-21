using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoreCMS.Application.UserType.Queries
{
    public class GetUserTypeOptionsQueryHandler : BaseDbContextHandler<GetUserTypeOptionsQuery, OptionViewModel[]>
    {
        public GetUserTypeOptionsQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(GetUserTypeOptionsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.UserType.Select(o => new OptionViewModel
            {
                Text = o.Description,
                Value = o.UserTypeId.ToString()
            });

            if ( !string.IsNullOrEmpty(request.Query)) {
                query = query.Where(o => o.Text.ToLower().Contains(request.Query.ToLower()));
            }

            return await query.ToArrayAsync();
        }
    }
}