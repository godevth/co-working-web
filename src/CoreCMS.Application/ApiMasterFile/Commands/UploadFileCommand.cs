using System;
using CoreCMS.Application.ApiMasterFile.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.ApiMasterFile.Commands
{
    public class UploadFileCommand : IRequest<CommandResult<UploadFileResponse>>
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Int16? Revision { get; set; }
        public byte[] Data { get; set; }
    }
}