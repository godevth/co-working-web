using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterData.Queries;
using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.Place.Queries
{
    public class GetWishlistQueryHandler : BaseDbContextHandler<GetWishlistQuery,SearchResult<WishlistViewModel>>
    {
        private readonly IdentityServerConfig _config;
        protected IMediator _mediator;
        public GetWishlistQueryHandler(ApplicationDbContext context, IOptions<IdentityServerConfig> config,IMediator mediator) : base(context)
        {
            _config = config.Value;
            _mediator = mediator;
        }


        public override async Task<SearchResult<WishlistViewModel>> Handle(GetWishlistQuery request, CancellationToken cancellationToken)
        {
            SearchResult<WishlistViewModel> result = new SearchResult<WishlistViewModel>();
            try
            {
                var placeId = _context.WishlistUserMapping.Where(o => o.UserId == request.UserId && o.IsWishlist).Select(o => o.PlaceId);

                var query = _context.Place.Where(o => !o.IsDeleted && !o.InActiveStatus && placeId.Contains(o.PlaceId));

                var finalQuery = query.Select(o => new WishlistViewModel
                {
                    PlaceId = o.PlaceId.ToString(),
                    PlaceName = request.Language == "TH" ? o.PlaceName_TH : o.PlaceName_EN,
                    Address = o.Address,
                    ProvinceId = o.PROVINCE_ID.Value,
                    ApmherId = o.AMPER_ID.Value,
                    TambonId = o.TAMBON_ID.Value,
                    Zipcode = o.ZIP_CODE,
                    IsWishlist = true,
                    Picture = _context.Picture.Where(x => x.TypeRef == "Room" & !x.IsDeleted && !x.InActiveStatus && o.Room.Select(o => o.RoomId.ToString()).Contains(x.CodeRef))
                    .Select(o => new Pictures()
                    {
                        Id = o.PictureId,
                        DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", o.FileInfoId),
                        DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", o.FileInfoId)
                    }).ToList()
                }).ToList();

                #region  Address
                var province = await _mediator.Send(new SearchMasterProvinceQuery());
                var ampher = await _mediator.Send(new SearchMasterAmpherQuery());
                var tambon = await _mediator.Send(new SearchMasterTumbolQuery());
                foreach(var item in finalQuery)
                {
                    var provinceName = province.Data.Where(o => o.ProvinceId == item.ProvinceId.ToString()).FirstOrDefault();
                    item.Province = request.Language == "EN"? provinceName.ProvinceEN : provinceName.ProvinceTH;

                    var ampherName = ampher.Data.Where(o => o.ProvinceId == item.ProvinceId.ToString() && o.AmpherId == item.ApmherId.ToString()).FirstOrDefault();
                    item.Ampher = request.Language == "EN"? ampherName.AmpherEN : ampherName.AmpherTH;

                    var tambonName = tambon.Data.Where(o => o.ProvinceId == item.ProvinceId.ToString() && o.AmpherId == item.ApmherId.ToString() && o.TumbolId == item.TambonId.ToString()).FirstOrDefault();
                    item.Tambon = request.Language == "EN"? tambonName.TumbolEN:tambonName.TumbolTH;
                }
                #endregion

                request.ItemsPerPage = request.ItemsPerPage > 0 ? request.ItemsPerPage : 20;
                request.Page = request.Page > 0 ? request.Page : 1;
                int page = request.Page - 1;

                result.Items =  finalQuery.OrderBy(o => o.PlaceName).Skip(page * request.ItemsPerPage).Take(request.ItemsPerPage).ToArray();
                result.Total = finalQuery.Count();

            }
            catch(Exception e)
            {
            }

            return await Task.FromResult<SearchResult<WishlistViewModel>>(result);
        }
    }
}