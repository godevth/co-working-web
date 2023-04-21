using CoreCMS.Application.Company.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using LinqKit;
using Microsoft.Extensions.Options;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Company.Queries
{
    public class GetCompanyQueryHandler : BaseDbContextHandler<GetCompanyQuery, CommandResult<CompanyView>>
    {
        private readonly IdentityServerConfig _config;
        public GetCompanyQueryHandler(ApplicationDbContext context, IOptions<IdentityServerConfig> config) : base(context)
        {
            _config = config.Value;
        }

        public override Task<CommandResult<CompanyView>> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Company
                .AsExpandable();

           CommandResult<CompanyView> result = new CommandResult<CompanyView>()
            {
                Data = null
            };

            var selectQuery = query.Where(o=>o.CompanyId==request.Id).Select(o => new CompanyView ()
             {
                 Id = o.CompanyId,
                 Name_TH =o.CompanyName_TH,
                 Name_EN =o.CompanyName_EN,
                 InActiveStatus = o.InActiveStatus,
                 Owner = o.OwnerId,
                 Profiles = o.CompanyProfiles.Where(p=>!p.IsDeleted).Select(p=> new CompanyProfileView(){
                     AdminId=p.AdminId,
                     PlaceId =p.PlaceId.ToString(),
                     CompanyProfileId =p.CompanyProfilesId
                 }).ToList()
             });
        result.Data=selectQuery.SingleOrDefault();
        result.Status = true;
        result.Message = "ดึงข้อมูลบริษัทสำเร็จ";
            return Task.FromResult<CommandResult<CompanyView>>(result);
        }
    }
}
