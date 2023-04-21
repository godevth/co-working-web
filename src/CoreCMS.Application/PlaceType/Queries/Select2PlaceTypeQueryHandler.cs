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

namespace CoreCMS.Application.PlaceType.Queries
{
    public class Select2PlaceTypeQueryHandler : BaseDbContextHandler<Select2PlaceTypeQuery,OptionViewModel[]>
    {
        public Select2PlaceTypeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2PlaceTypeQuery request, CancellationToken cancellationToken)
        {
            var query = _context.PlaceType.Where(o=>!o.IsDeleted)
                .AsExpandable();

           OptionViewModel[] result = null;
           
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;
            if(request.IsAll){
                query = query.Where(o=>!o.InActiveStatus);
            }
            var selectQuery = query.Where(o=>o.PlaceTypeName.Contains(search)).Select(o => new OptionViewModel
            {
                Value = o.PlaceTypeID.ToString(),
                Text = request.Language == "EN" ? o.PlaceTypeNameEN != null ? o.PlaceTypeNameEN:o.PlaceTypeName :o.PlaceTypeName
            });

            result = await selectQuery.ToArrayAsync();
        
            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
