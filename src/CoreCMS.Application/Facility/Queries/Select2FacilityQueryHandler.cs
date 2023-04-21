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

namespace CoreCMS.Application.Facility.Queries
{
    public class Select2FacilityQueryHandler : BaseDbContextHandler<Select2FacilityQuery, OptionViewModel[]>
    {
        public Select2FacilityQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2FacilityQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Facility.Where(o => !o.IsDeleted)
                .AsExpandable();

            OptionViewModel[] result = null;
            if (request.IsAll)
            {
                query = query.Where(o => !o.InActiveStatus);
            }
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;

            var selectQuery = query.Where(o => o.FacilityName_TH.Contains(search) || o.FacilityName_EN.Contains(search)).Select(o => new OptionViewModel
            {
                Value = o.FacilityId.ToString(),
                Text = o.FacilityName_TH,
            });

            result = await selectQuery.ToArrayAsync();

            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
