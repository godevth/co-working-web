using CoreCMS.Application.ApiMasterFile.Models;
using MediatR;

namespace CoreCMS.Application.ApiMasterFile.Commands
{
    public class DownloadFileCommand : IRequest<DownloadResponse>
    {
        public string FileInfoId { get; set; }
    }
}