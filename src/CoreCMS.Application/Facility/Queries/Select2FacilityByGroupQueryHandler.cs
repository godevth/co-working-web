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
    public class Select2FacilityByGroupQueryHandler : BaseDbContextHandler<Select2FacilityByGroupQuery, OptionViewModel[]>
    {
        public Select2FacilityByGroupQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(Select2FacilityByGroupQuery request, CancellationToken cancellationToken)
        {

            var query = _context.FacilityServices.Where(o => !o.IsDeleted)
    .AsExpandable();
            OptionViewModel[] result = null;

            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;
            if (Convert.ToInt32(request.FacilityGroup) == 0)
            {

                var selectQuery = query.Where(o => o.RoomId == new Guid(request.Id) && (o.Facility.FacilityName_TH.Contains(search) || o.Facility.FacilityName_EN.Contains(search))).Select(o => new OptionViewModel
                {
                    Value = o.FacilityServicesId.ToString(),
                    Text = request.Language == "EN" ? o.Facility.FacilityName_EN: o.Facility.FacilityName_TH,
                    Data = new
                    {
                        IsAll = true,
                        Price = o.Price
                    }
                });
                result = await selectQuery.Distinct().ToArrayAsync();
            }
            else
            {
                var selectQuery = query.Where(o => o.PlaceId == new Guid(request.Id) &&( o.Facility.FacilityName_TH.Contains(search) || o.Facility.FacilityName_EN.Contains(search))).Select(o => new OptionViewModel
                {
                    Value = o.FacilityServicesId.ToString(),
                    Text = request.Language == "EN" ? o.Facility.FacilityName_EN: o.Facility.FacilityName_TH,
                    Data = new
                    {
                        IsAll = true,
                        Price = o.Price
                    }
                });
                result = await selectQuery.Distinct().ToArrayAsync();
            }




            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
