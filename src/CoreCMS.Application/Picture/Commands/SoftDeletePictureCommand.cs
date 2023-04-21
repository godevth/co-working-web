using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Picture.Commands
{
    public class SoftDeletePictureCommand : IRequest<CommandResult<bool>>
    {
        public string CodeRef { get; set; }
        public string TypeRef { get; set; }
        public string DeleteUserId { get; set; }
        public string FileInfoId { get; set;}
    }
}