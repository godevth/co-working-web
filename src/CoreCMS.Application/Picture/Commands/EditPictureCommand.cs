using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Picture.Commands
{
    public class EditPictureCommand : IRequest<CommandResult<int>>
    {
        public string FileBase64 { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string CodeRef { get; set; }
        public string TypeRef { get; set; }
        public string UpdatedUserId { get; set; }
    }
}