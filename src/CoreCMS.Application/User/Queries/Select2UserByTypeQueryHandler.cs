using CoreCMS.Application.User.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.User.Queries
{
    public class Select2UserByTypeQueryHandler : BaseDbContextHandler<Select2UserByTypeQuery,OptionViewModel[]>
    {
        public Select2UserByTypeQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2UserByTypeQuery request, CancellationToken cancellationToken)
        {
            var query = _context.UserSearchView
                .AsExpandable();
           OptionViewModel[] result = null;
           
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;

            var selectQuery = query.Where(o=>!o.IsDeleted&&o.DisplayName.Contains(search)&&o.UserTypeId.Value==request.UserType).Select(o => new OptionViewModel
            {
                Value = o.Id.ToString(),
                Text =o.DisplayName,
            });

            result = await selectQuery.ToArrayAsync();
        
            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
