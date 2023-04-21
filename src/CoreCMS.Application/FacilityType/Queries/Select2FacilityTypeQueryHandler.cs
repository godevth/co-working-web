using CoreCMS.Application.PlaceType.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.FacilityType.Queries
{
    public class Select2FacilityTypeQueryHandler : BaseDbContextHandler<Select2FacilityTypeQuery,OptionViewModel[]>
    {
        public Select2FacilityTypeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2FacilityTypeQuery request, CancellationToken cancellationToken)
        {
            var query = _context.FacilityType.Where(o=>!o.IsDeleted)
                .AsExpandable();
            if(request.IsAll){
                query = query.Where(o=>!o.InActiveStatus);
            }
           OptionViewModel[] result = null;
           
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;

            var selectQuery = query.Where(o=>o.FacilityTypeName_TH.Contains(search)||o.FacilityTypeName_EN.Contains(search)).Select(o => new OptionViewModel
            {
                Value = o.FacilityTypeID.ToString(),
                Text =o.FacilityTypeName_TH,
            });

            result = await selectQuery.ToArrayAsync();
        
            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
