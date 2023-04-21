using CoreCMS.Application.Facility.Models;
using CoreCMS.Application.Room.Models;
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

namespace CoreCMS.Application.Facility.Queries
{
    public class GetFacilityQueryHandler : BaseDbContextHandler<GetFacilityQuery,CommandResult<FacilityView>>
    {
        private readonly IdentityServerConfig _config;
        public GetFacilityQueryHandler(ApplicationDbContext context,IOptions<IdentityServerConfig> config) : base(context)
        {
             _config = config.Value;
        }

        public override Task<CommandResult<FacilityView>> Handle(GetFacilityQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Facility
                .AsExpandable();

           CommandResult<FacilityView> result = new CommandResult<FacilityView>()
            {
                Data = null
            };

            var selectQuery = query.Where(o=>o.FacilityId == request.Id).Select(o => new FacilityView
            {
                Id = o.FacilityId,
                Name_TH = o.FacilityName_TH,
                Name_EN = o.FacilityName_EN,
                FacilityTypeId =o.FacilityTypeID.ToString(),
                InActiveStatus =o.InActiveStatus,
                Pictures = _context.Picture.Where(p=>p.TypeRef==PictureTypeRef.Facility&&p.CodeRef==request.Id.ToString()&&!p.IsDeleted).Select(p=>new Pictures(){
                    Id = p.PictureId,
                    DownloadFileBase64Url = p.FileInfoId,
                    DownloadFileByteUrl = p.FileInfoId
                }).ToList()
            });

            result.Data = selectQuery.Single();

            if(result.Data.Pictures.Count>0){
                foreach(var item in result.Data.Pictures){
                      item.DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", item.DownloadFileBase64Url);
                    item.DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", item.DownloadFileByteUrl);
                }
            }
        
            return Task.FromResult<CommandResult<FacilityView>>(result);
        }
    }
}
