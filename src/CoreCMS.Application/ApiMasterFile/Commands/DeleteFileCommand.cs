using MediatR;

namespace CoreCMS.Application.ApiMasterFile.Commands
{
    public class DeleteFileCommand : IRequest<bool>
    {
        public string FileInfoId { get; set; }
    }
}