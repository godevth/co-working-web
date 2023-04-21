using CoreCMS.Application.RoomType.Models;
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

namespace CoreCMS.Application.Place.Queries
{
    public class Select2PlaceByCompanyQueryHandler : BaseDbContextHandler<Select2PlaceByCompanyQuery, OptionViewModel[]>
    {
        public Select2PlaceByCompanyQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2PlaceByCompanyQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Place
                .AsExpandable();
           OptionViewModel[] result = null;
           
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;
            var selectQuery = query.Where(o => !o.IsDeleted && !o.InActiveStatus && o.PlaceName_TH.Contains(search) && o.CompanyId==Convert.ToInt32(request.CompanyId)).Select(o => new OptionViewModel
            {
                Value = o.PlaceId.ToString(),
                Text =o.PlaceName_TH,
            });

            result = await selectQuery.ToArrayAsync();
        
            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
