using CoreCMS.Application.ApiMasterFile.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Picture.Commands
{
    public class AddPictureCommandHandler : BaseDbContextWithMediatorHandler<AddPictureCommand, CommandResult<int>>
    {
        private readonly ApiConfig _config;
        private readonly ILogger<AddPictureCommandHandler> _logger;

        public AddPictureCommandHandler(ApplicationDbContext context, IOptions<ApiConfig> config, IMediator mediator, ILogger<AddPictureCommandHandler> logger) : base(context, mediator)
        {
            _config = config.Value;
            _logger = logger;
        }

        public override async Task<CommandResult<int>> Handle(AddPictureCommand request, CancellationToken cancellationToken)
        {
            CommandResult<int> cmdResult = new CommandResult<int>()
            {
                Message = "ไม่สามารถเพิ่มข้อมูลไฟล์ได้"
            };

            if (string.IsNullOrEmpty(request.FileBase64))
            {
                throw new Exception("กรุณาระบุ FileBase64");
            }

            try
            {
                byte[] dataPic = Convert.FromBase64String(request.FileBase64);
                if (dataPic.Length > 0)
                {
                    #region CallApiUploadFile 
                    UploadFileCommand uploadFileCommand = new UploadFileCommand()
                    {
                        Data = dataPic,
                        FileName = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds().ToString() + request.FileName,
                        ContentType = request.ContentType
                    };
                    var uploadFileResult = await _mediator.Send(uploadFileCommand);
                    #endregion

                    #region InsertPicture
                    if (uploadFileResult.Status && uploadFileResult.Data != null)
                    {
                        Domain.Picture picture = new Domain.Picture()
                        {
                            FileName = request.FileName,
                            CodeRef = request.CodeRef,
                            TypeRef = request.TypeRef,
                            GridFsId = uploadFileResult.Data.Result.GridFsId,
                            FileInfoId = uploadFileResult.Data.Result.FileInfoId,
                            SysName = uploadFileResult.Data.Result.SysName,
                            DownloadUrl = uploadFileResult.Data.Result.DownloadUrl,
                            CreatedDate = DateTime.Now,
                            CreatedUserId = request.CreateUserId
                        };

                        _context.Add(picture);
                        bool status = _context.SaveChanges() > 0;
                        if(status)
                        {
                            cmdResult.Status = status;
                            cmdResult.Data = picture.PictureId;
                            cmdResult.Message = "เพิ่มข้อมูลไฟล์สำเร็จ";
                        }
                    }
                    #endregion
                }
            }
            catch(Exception e)
            {
                _logger.LogInformation("Exception is {@e}", e);
                throw e;
            }

            return await Task.FromResult(cmdResult);
        }
    }
}
