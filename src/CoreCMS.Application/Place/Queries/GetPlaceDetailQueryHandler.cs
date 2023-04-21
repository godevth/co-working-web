using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Place.Queries
{
    public class GetPlaceDetailQueryHandler : BaseDbContextHandler<GetPlaceDetailQuery, CommandResult<PlaceModels>>
    {
        private readonly IdentityServerConfig _config;
        public GetPlaceDetailQueryHandler(ApplicationDbContext context, IOptions<IdentityServerConfig> config) : base(context)
        {
            _config = config.Value;
        }
        public async override Task<CommandResult<PlaceModels>> Handle(GetPlaceDetailQuery request, CancellationToken cancellationToken)
        {
            CommandResult<PlaceModels> result = new CommandResult<PlaceModels>();
            var query = _context.Place
                .Include(o => o.SeeingType)
                .Where(o => !o.IsDeleted && o.PlaceId == request.Id).Single();

            if (query==null) {
                result.Message = "ไม่พบข้อมูลที่เลือก";
                return await Task.FromResult<CommandResult<PlaceModels>>(result);
            }

            PlaceModels items = new PlaceModels() {
                PlaceId = query.PlaceId,
                Name_TH = query.PlaceName_TH,
                Name_EN = query.PlaceName_EN,
                PlaceTypeId = query.PlaceTypeID.ToString(),
                ServiceItems = _context.FacilityServices.Where(o => !o.IsDeleted && o.PlaceId == request.Id).Select(o=>new FacilityServiceItems() {
                FacilityIds=o.FacilityId.ToString(),
                FacilityServicesId = o.FacilityServicesId,
                Qty =o.Qty.ToString(),
                Price = o.Price.ToString()
                }
                ).ToList(),
                DateItems =  _context.ImplementationDate.Where(o => !o.IsDeleted && o.PlaceId == request.Id).Select(o=>new ImplementationDateItems() {
                ImplementationDateId = o.ImplementationDateId,
                StartDay =o.StartDay,
                StartTime =o.StartTime,
                EndTime =o.EndTime
                }).ToList(),
                InActiveStatus = query.InActiveStatus,
                ProvinceId = query.PROVINCE_ID.ToString(),
                AmpherId = query.AMPER_ID.ToString(),
                Address = query.Address,
                Latitude = query.Latitude,
                Longitude = query.Longitude,
                TambolId = query.TAMBON_ID.ToString(),
                Zipcode = query.ZIP_CODE,
                NearBy = query.NearBy,
                Details = query.Details,
                CompanyId = query.CompanyId.ToString(),
                Seeing = query.SeeingTypeCode,
                SeeingName = query.SeeingType.SystemVariableName,
                Domains = _context.Privilege.Where(o => !o.IsDeleted && !o.InActiveStatus 
                    && o.PlaceId == request.Id && o.PrivilegeTypeCode == "PRIVILEGE_TYPE_DOMAIN")
                    .Select(o => o.Domain).ToList(),
                PaymentMethodItems = _context.PlacePaymentMethod.Where(o => o.PlaceId == request.Id)
                    .Select(o => new PaymentMethodItems()
                    {
                        Value = o.PaymentMethodCode,
                        Text = o.PaymentMethod.SystemVariableName
                    }).ToList(),
                Pictures = _context.Picture.Where(p=>p.TypeRef==PictureTypeRef.UploadFileByPlace&&p.CodeRef==request.Id.ToString()&&!p.IsDeleted).Select(p=>new Pictures(){
                    Id = p.PictureId,
                    FileName =p.FileName,
                    DownloadFileBase64Url = p.FileInfoId,
                    DownloadFileByteUrl = p.FileInfoId,
                }).ToList(),
                GP = query.GP,
                SubMerchantId = query.SubMerchantId
            };

            if(items.Pictures.Count>0){
                foreach(var item in items.Pictures){
                      item.DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", item.DownloadFileBase64Url);
                    item.DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}&filename={3}", _config.Authority, "ByteFile", item.DownloadFileByteUrl,item.FileName);
                }
            }

            result.Data = items;
            result.Status = true;
            return await Task.FromResult<CommandResult<PlaceModels>>(result);
        }
    }
}
