using CoreCMS.Application.Room.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using LinqKit;
using Microsoft.Extensions.Options;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Room.Queries
{
    public class GetPlaceGPQueryHandler : BaseDbContextHandler<GetPlaceGPQuery,CommandResult<decimal>>
    {
        private readonly IdentityServerConfig _config;
        public GetPlaceGPQueryHandler(ApplicationDbContext context, IOptions<IdentityServerConfig> config) : base(context)
        {
            _config = config.Value;
        }

        public override Task<CommandResult<decimal>> Handle(GetPlaceGPQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Room.Where(o => o.Place.PlaceId == request.Id).FirstOrDefault();

            CommandResult<decimal> result = new CommandResult<decimal>()
            {
                Data = 0
            };
            
            result.Data = _context.Place.Where(o => o.PlaceId == query.PlaceId).Select(o => o.GP).FirstOrDefault();

            return Task.FromResult<CommandResult<decimal>>(result);
        }
    }
}
