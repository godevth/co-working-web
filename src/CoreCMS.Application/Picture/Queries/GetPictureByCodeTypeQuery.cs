using CoreCMS.Application.Picture.Models;
using MediatR;

namespace CoreCMS.Application.Picture.Queries
{
    public class GetPictureByCodeTypeQuery : IRequest<PictureView>
    {
        public string CodeRef { get; set; }
        public string TypeRef { get; set; }
    }
}