using CoreCMS.Application.Facility.Models;
using CoreCMS.Application.Facility.Queries;
using CoreCMS.Application.Role.Models;
using CoreCMS.Application.Room.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using LinqKit;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Role.Queries
{
    public class GetRoleQueryHandler : BaseDbContextHandler<GetRoleQuery, CommandResult<RoleView>>
    {
        private readonly IdentityServerConfig _config;
        public GetRoleQueryHandler(ApplicationDbContext context,IOptions<IdentityServerConfig> config) : base(context)
        {
             _config = config.Value;
        }

        public override Task<CommandResult<RoleView>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Roles.AsExpandable();

           CommandResult<RoleView> result = new CommandResult<RoleView>()
            {
                Data = null
            };

            var selectQuery = query.Where(o=>o.Id == request.Id).Select(o => new RoleView
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description,
                UserTypeId = o.UserTypeId,
                UserTypeName = o.UserType.Description
            });

            result.Data = selectQuery.Single();
        
            return Task.FromResult<CommandResult<RoleView>>(result);
        }
    }
}
