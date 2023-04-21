using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.PlaceTheme.Models;
using CoreCMS.Application.Privilege.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.PlaceTheme.Queries
{
    public class GetPlaceThemeByUserIdQueryHandler : BaseDbContextWithMediatorHandler<GetPlaceThemeByUserIdQuery, SearchMobileResult<PlaceThemeView>>
    {
        private readonly IdentityServerConfig _config;

        public GetPlaceThemeByUserIdQueryHandler(ApplicationDbContext context, IMediator mediator, IOptions<IdentityServerConfig> config) : base(context, mediator)
        {
            _config = config.Value;
        }

        public override async Task<SearchMobileResult<PlaceThemeView>> Handle(GetPlaceThemeByUserIdQuery request, CancellationToken cancellationToken)
        {
            request.ItemsPerPage = request.ItemsPerPage > 0 ? request.ItemsPerPage : 20;
            request.Page = request.Page > 0 ? request.Page : 1;
            int page = request.Page - 1;

            var query = _context.PlaceTheme.Where(o => !o.IsDeleted && !o.InActiveStatus);

            if(string.IsNullOrEmpty(request.UserId))
            {
                var tmps = new SearchMobileResult<PlaceThemeView>();
                return await Task.FromResult(tmps);
            }

            #region Get Place ตามสิทธิ์ของ User
            GetPlaceByUserIdQuery getPlaceByUser = new GetPlaceByUserIdQuery()
            {
                UserId = request.UserId
            };
            var placeByUser = await _mediator.Send(getPlaceByUser);
            
            //user submit consent แล้ว
            if(placeByUser.PlaceIds.Any())
            {
                query = query.Where(o => placeByUser.PlaceIds.Contains(o.PlaceId));
            }
            //user ไม่ submit consent หรือ user public
            else
            {
                var tmps = new SearchMobileResult<PlaceThemeView>();
                return await Task.FromResult(tmps);
            }
            #endregion

            var themeTypes = _context.SystemVariables
                .Where(o => o.SystemVariableCategoryCode == "THEME_TYPE")
                .Select(o => o.SystemVariableCode)
                .ToList();
            
            var count = query.Count();
            var sQuery = query.Select(o => new PlaceThemeView()
                    {
                        ThemeId = o.ThemeId,
                        Name = o.Name,
                        Description = o.Description,
                        PlaceId = o.PlaceId,
                        PlaceName = request.Language == "EN" ? o.Place.PlaceName_EN : o.Place.PlaceName_TH,
                        Pictures = _context.Picture.Where(p => !p.IsDeleted && !p.InActiveStatus 
                            && themeTypes.Contains(p.TypeRef) && p.CodeRef == o.ThemeId.ToString())
                            .Select(p => new PictureView()
                            {
                                Id = p.PictureId,
                                Type = p.TypeRef,
                                DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", p.FileInfoId),
                                DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", p.FileInfoId)
                            }).ToList(),
                        IsSelect = _context.UserTheme.Where(u => !u.IsDeleted && !u.InActiveStatus && u.ThemeId == o.ThemeId && u.UserId == request.UserId).Any()
                    });

            var items = await sQuery.Skip(page * request.ItemsPerPage).Take(request.ItemsPerPage).ToArrayAsync();
            var result = new SearchMobileResult<PlaceThemeView>(){
                More = (request.Page * request.ItemsPerPage) < count,
                Total = sQuery.Count(),
                Items = items
            };

            return await Task.FromResult(result);
        }
    }
}