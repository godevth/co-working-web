using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Picture.Queries
{
    public class GetPictureByCodeTypeQueryHandler : BaseDbContextWithMediatorHandler<GetPictureByCodeTypeQuery, PictureView>
    {
        public GetPictureByCodeTypeQueryHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<PictureView> Handle(GetPictureByCodeTypeQuery request, CancellationToken cancellationToken)
        {
            var picture = _context.Picture
                .Where(o => !o.IsDeleted && o.CodeRef == request.CodeRef && o.TypeRef == request.TypeRef)
                .Select(o => new PictureView()
                {
                    TypeRef = o.TypeRef,
                    CodeRef = o.CodeRef,
                    SysName = o.SysName,
                    FileInfoId = o.FileInfoId,
                    DownloadUrl = o.DownloadUrl,
                    FileName = o.FileName,
                    GridFsId = o.GridFsId,
                    PictureId = o.PictureId
                }).FirstOrDefault();

            return await Task.FromResult(picture);
        }
    }
}