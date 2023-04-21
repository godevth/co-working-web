using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Picture.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.User.Queries
{
    public class GetUserProfileQueryHandler : BaseDbContextWithMediatorHandler<GetUserProfileQuery, UserProfile>
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IdentityServerConfig _config;
        public GetUserProfileQueryHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager, IOptions<IdentityServerConfig> config) : base(context, mediator)
        {
            _userManager = userManager;
            _config = config.Value;
        }

        public override async Task<UserProfile> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            #region GetPicture
            GetPictureByCodeTypeQuery getPicture = new GetPictureByCodeTypeQuery()
            {
                CodeRef = request.UserId,
                TypeRef = PictureTypeRef.User
            };
            var picture = await _mediator.Send(getPicture);

            #endregion
            UserProfile model = null;

            var user = _userManager.FindByIdAsync(request.UserId);



            if (user != null)
            {
                model = new UserProfile()
                {
                    FirstName = user.Result.FirstName,
                    LastName = user.Result.LastName,
                    Email = user.Result.Email,
                    PhoneNumber = user.Result.PhoneNumber,
                    Gender = user.Result.Gender,
                    BirthDate= user.Result.BirthDate,
                    Address = user.Result.Address,
                    TumbolId = user.Result.TumbolId,
                    AmphurId = user.Result.AmphurId,
                    ProvinceId = user.Result.ProvinceId,
                    PostCode = user.Result.PostCode,
                    Companyname = user.Result.Companyname,
                    PhoneCountryCode = user.Result.PhoneCountryCode
                };
                // if(picture != null)
                // {
                //     model.DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", picture.FileInfoId);
                //     model.DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", picture.FileInfoId);
                // }
            }
            return await Task.FromResult(model);
        }
    }
}