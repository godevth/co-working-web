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

namespace CoreCMS.Application.RoomType.Queries
{
    public class Select2RoomTypeQueryHandler : BaseDbContextHandler<Select2RoomTypeQuery,OptionViewModel[]>
    {
        public Select2RoomTypeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2RoomTypeQuery request, CancellationToken cancellationToken)
        {
            var query = _context.RoomType.Where(o=>!o.IsDeleted)
                .AsExpandable();
           OptionViewModel[] result = null;
            if(request.IsAll){
                query = query.Where(o=>!o.InActiveStatus);
            }
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;

            var selectQuery = query.Where(o=>o.RoomTypeName.Contains(search)).Select(o => new OptionViewModel
            {
                Value = o.RoomTypeID.ToString(),
                Text =request.Language == "EN" ? o.RoomTypeNameEN != null ? o.RoomTypeNameEN : o.RoomTypeName :o.RoomTypeName,
            });

            result = await selectQuery.ToArrayAsync();
        
            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
