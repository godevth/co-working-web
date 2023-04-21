using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoreCMS.Application.Role.Queries
{
    public class GetRoleOptionsQueryHandler : BaseDbContextHandler<GetRoleOptionsQuery, OptionViewModel[]>
    {
        public GetRoleOptionsQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(GetRoleOptionsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Roles.Where(o=>o.UserTypeId!=0);
            if(request.UserTypeId!=null){
                query = query.Where(o=>o.UserTypeId==Convert.ToInt32(request.UserTypeId));
            }

          var res = query.Select(o => new OptionViewModel
            {
                Text = o.Description,
                Value = o.Name
            });

            if ( !string.IsNullOrEmpty(request.Query)) {
                res = res.Where(o => o.Text.ToLower().Contains(request.Query.ToLower()));
            }

            return await res.ToArrayAsync();
        }
    }
}