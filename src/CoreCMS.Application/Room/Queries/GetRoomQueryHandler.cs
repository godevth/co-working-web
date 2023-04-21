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

namespace CoreCMS.Application.Room.Queries
{
    public class GetRoomQueryHandler : BaseDbContextHandler<GetRoomQuery,CommandResult<RoomModels>>
    {
        private readonly IdentityServerConfig _config;
        public GetRoomQueryHandler(ApplicationDbContext context, IOptions<IdentityServerConfig> config) : base(context)
        {
            _config = config.Value;
        }

        public override Task<CommandResult<RoomModels>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Room
                .AsExpandable();

           CommandResult<RoomModels> result = new CommandResult<RoomModels>()
            {
                Data = null
            };

            var selectQuery = query.Where(o=>o.RoomId==request.Id).Select(o => new RoomModels
            {
                Id = o.RoomId,
                Name_EN = o.RoomName_EN,
                Name_TH =o.RoomName_TH,
                Detail_TH = o.Detail_TH,
                Detail_EN = o.Detail_EN,
                //Price =o.Price.ToString(),
                Capacity =o.Capacity.ToString(),
                //Qty = o.RoomQty.ToString(),
                PlaceId =o.PlaceId,
                RoomTypeId =o.RoomTypeId.ToString(),
                ServiceItems =_context.FacilityServices.Where(s => !s.IsDeleted && s.RoomId == request.Id)
                .Select(s=>new FacilityServiceItems() {
                FacilityIds=s.FacilityId.ToString(),
                FacilityServicesId = s.FacilityServicesId,
                Qty =s.Qty.ToString(),
                Price = s.Price.ToString()
                }).ToList(),
                Pictures = _context.Picture.Where(p=>p.TypeRef==PictureTypeRef.Room&&p.CodeRef==request.Id.ToString()&&!p.IsDeleted).Select(p=>new Pictures(){
                    Id = p.PictureId,
                    DownloadFileBase64Url = p.FileInfoId,
                    DownloadFileByteUrl = p.FileInfoId
                }).ToList(),
                InActiveStatus =o.InActiveStatus,
                Price = _context.RoomPrice.Where(s => !s.IsDeleted && s.RoomId == request.Id)
                .Select(p=>new PriceItem(){
                    Id = p.Id,
                    Price = p.Price.ToString(),
                    Qty = p.Qty.ToString(),
                    TimeType = p.TimeType
                }).ToList(),
                GP = o.Place.GP
            });

            result.Data = selectQuery.Single();

            if(result.Data.Pictures.Count>0){
                foreach(var item in result.Data.Pictures){
                      item.DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", item.DownloadFileBase64Url);
                    item.DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", item.DownloadFileByteUrl);
                }
            }
        
            return Task.FromResult<CommandResult<RoomModels>>(result);
        }
    }
}
