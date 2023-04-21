using CoreCMS.Application.Room.Models;
using CoreCMS.Application.RoomType.Models;
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

namespace CoreCMS.Application.RoomType.Queries
{
    public class GetRoomTypeQueryHandler : BaseDbContextHandler<GetRoomTypeQuery,CommandResult<RoomTypeView>>
    {
        private readonly IdentityServerConfig _config;
        public GetRoomTypeQueryHandler(ApplicationDbContext context,IOptions<IdentityServerConfig> config) : base(context)
        {
            _config = config.Value;
        }

        public override Task<CommandResult<RoomTypeView>> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
        {
            var query = _context.RoomType
                .AsExpandable();

           CommandResult<RoomTypeView> result = new CommandResult<RoomTypeView>()
            {
                Data = null
            };

            var selectQuery = query.Where(o=>o.RoomTypeID==request.Id).Select(o => new RoomTypeView
            {
                Id = o.RoomTypeID,
                Name = o.RoomTypeName,
                NameEN = o.RoomTypeNameEN,
                InActiveStatus =o.InActiveStatus,
                Picture = _context.Picture.Where(p=>p.TypeRef==PictureTypeRef.RoomType&&p.CodeRef==request.Id.ToString()&&!p.IsDeleted).Select(p=>new Pictures()
                {
                    Id = p.PictureId,
                    DownloadFileBase64Url = p.FileInfoId,
                    DownloadFileByteUrl = p.FileInfoId
                }).FirstOrDefault()
            });

            result.Data = selectQuery.Single();
            if(result.Data.Picture != null){
                
                result.Data.Picture.DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", result.Data.Picture.DownloadFileBase64Url);
                result.Data.Picture.DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", result.Data.Picture.DownloadFileByteUrl);
                
            }
        
            return Task.FromResult<CommandResult<RoomTypeView>>(result);
        }
    }
}
