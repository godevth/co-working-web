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

namespace CoreCMS.Application.Company.Queries
{
    public class Select2CompanyQueryHandler : BaseDbContextHandler<Select2CompanyQuery, OptionViewModel[]>
    {
        public Select2CompanyQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2CompanyQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Company
                .AsExpandable();
           OptionViewModel[] result = null;
           
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;

             query = query.Where(o=>!o.IsDeleted&&o.CompanyName_TH.Contains(search));
            if(request.RoleName!="super_admin"){
                query = query.Where(o=>o.OwnerId==request.UserId);
            }

            var selectQuery = query
            .Select(o => new OptionViewModel
            {
                Value = o.CompanyId.ToString(),
                Text =o.CompanyName_TH,
            });

            result = await selectQuery.ToArrayAsync();
        
            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
