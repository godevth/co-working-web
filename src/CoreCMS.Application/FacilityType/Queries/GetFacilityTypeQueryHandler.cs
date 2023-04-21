using CoreCMS.Application.FacilityType.Models;
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

namespace CoreCMS.Application.FacilityType.Queries
{
    public class GetFacilityTypeQueryHandler : BaseDbContextHandler<GetFacilityTypeQuery,CommandResult<FacilityTypeView>>
    {
        public GetFacilityTypeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<CommandResult<FacilityTypeView>> Handle(GetFacilityTypeQuery request, CancellationToken cancellationToken)
        {
            var query = _context.FacilityType
                .AsExpandable();

           CommandResult<FacilityTypeView> result = new CommandResult<FacilityTypeView>()
            {
                Data = null
            };

            var selectQuery = query.Where(o=>o.FacilityTypeID == request.Id).Select(o => new FacilityTypeView
            {
                Id = o.FacilityTypeID,
                Name_TH = o.FacilityTypeName_TH,
                Name_EN = o.FacilityTypeName_EN,
                InActiveStatus =o.InActiveStatus
            });

            result.Data = selectQuery.Single();
        
            return Task.FromResult<CommandResult<FacilityTypeView>>(result);
        }
    }
}
