using CoreCMS.Application.Picture.Models;
using MediatR;

namespace CoreCMS.Application.Picture.Queries
{
    public class GetPictureByFileInfoIdQuery : IRequest<PictureView>
    {
        public string FileInfoId { get; set; }
    }
}