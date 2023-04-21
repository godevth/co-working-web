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
    public class Select2PlaceQueryHandler : BaseDbContextHandler<Select2PlaceQuery,OptionViewModel[]>
    {
        public Select2PlaceQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2PlaceQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Place
                .AsExpandable();
           OptionViewModel[] result = null;
           
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;
            if (request.PlaceTypeId!=0)
            {
                query = query.Where(o => o.PlaceTypeID==request.PlaceTypeId);
            }
            var selectQuery = query.Where(o => !o.IsDeleted && !o.InActiveStatus && o.PlaceName_TH.Contains(search)).Select(o => new OptionViewModel
            {
                Value = o.PlaceId.ToString(),
                Text =request.Language == "EN" ? o.PlaceName_EN != null ? o.PlaceName_EN : o.PlaceName_TH :o.PlaceName_TH,
            });

            result = await selectQuery.ToArrayAsync();
        
            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
