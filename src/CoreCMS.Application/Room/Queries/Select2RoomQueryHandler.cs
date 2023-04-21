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

namespace CoreCMS.Application.Room.Queries
{
    public class Select2RoomQueryHandler : BaseDbContextHandler<Select2RoomQuery,OptionViewModel[]>
    {
        public Select2RoomQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2RoomQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Room
                .AsExpandable();
           OptionViewModel[] result = null;
           
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;

            #region PlaceId
            if (request.PlaceId.HasValue)
            {
                query = query.Where(o => o.PlaceId == request.PlaceId.Value);
            }
            #endregion

            var selectQuery = query.Where(o => !o.IsDeleted && !o.InActiveStatus && o.RoomName_TH.Contains(search)).Select(o => new OptionViewModel
            {
                Value = o.RoomId.ToString(),
                Text =o.RoomName_TH,
            });

            result = await selectQuery.ToArrayAsync();
        
            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
