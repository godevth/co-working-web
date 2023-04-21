using CoreCMS.Application.PlaceType.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using LinqKit;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.PlaceType.Queries
{
    public class GetPlaceTypeQueryHandler : BaseDbContextHandler<GetPlaceTypeQuery,CommandResult<PlaceTypeView>>
    {
        public GetPlaceTypeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<CommandResult<PlaceTypeView>> Handle(GetPlaceTypeQuery request, CancellationToken cancellationToken)
        {
            var query = _context.PlaceType
                .AsExpandable();

           CommandResult<PlaceTypeView> result = new CommandResult<PlaceTypeView>()
            {
                Data = null
            };

            var selectQuery = query.Where(o=>o.PlaceTypeID == request.Id).Select(o => new PlaceTypeView
            {
                Id = o.PlaceTypeID,
                Name = o.PlaceTypeName,
                NameEN = o.PlaceTypeNameEN,
                InActiveStatus =o.InActiveStatus
            });

            result.Data = selectQuery.Single();
        
            return Task.FromResult<CommandResult<PlaceTypeView>>(result);
        }
    }
}
