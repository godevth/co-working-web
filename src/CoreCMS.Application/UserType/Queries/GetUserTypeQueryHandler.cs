using CoreCMS.Application.UserType.Models;
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

namespace CoreCMS.Application.UserType.Queries
{
    public class GetUserTypeQueryHandler : BaseDbContextHandler<GetUserTypeQuery, CommandResult<UserTypeView>>
    {
        private readonly IdentityServerConfig _config;
        public GetUserTypeQueryHandler(ApplicationDbContext context,IOptions<IdentityServerConfig> config) : base(context)
        {
             _config = config.Value;
        }

        public override Task<CommandResult<UserTypeView>> Handle(GetUserTypeQuery request, CancellationToken cancellationToken)
        {
            var query = _context.UserType.AsExpandable();

           CommandResult<UserTypeView> result = new CommandResult<UserTypeView>()
            {
                Data = null
            };

            var selectQuery = query.Where(o=>o.UserTypeId == request.Id).Select(o => new UserTypeView
            {
                Id = o.UserTypeId,
                Name = o.Name,
                Description = o.Description,
            });

            result.Data = selectQuery.Single();
        
            return Task.FromResult<CommandResult<UserTypeView>>(result);
        }
    }
}
