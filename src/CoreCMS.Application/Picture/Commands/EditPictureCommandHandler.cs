using CoreCMS.Application.ApiMasterFile.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Picture.Commands
{
    public class EditPictureCommandHandler : BaseDbContextWithMediatorHandler<EditPictureCommand, CommandResult<int>>
    {
        private readonly ApiConfig _config;
        private readonly ILogger<EditPictureCommandHandler> _logger;

        public EditPictureCommandHandler(ApplicationDbContext context, IOptions<ApiConfig> config, IMediator mediator, ILogger<EditPictureCommandHandler> logger) : base(context, mediator)
        {
            _config = config.Value;
            _logger = logger;
        }

        public override async Task<CommandResult<int>> Handle(EditPictureCommand request, CancellationToken cancellationToken)
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
                        FileName = request.FileName,
                        ContentType = request.ContentType
                    };
                    var uploadFileResult = await _mediator.Send(uploadFileCommand);
                    #endregion


                    
                    #region Updateicture

                    var query = _context.Picture.Where(o => o.CodeRef == request.CodeRef).Single();
                    query.FileName = request.FileName;
                    query.TypeRef = request.TypeRef;
                    query.GridFsId = uploadFileResult.Data.Result.GridFsId;
                    query.FileInfoId = uploadFileResult.Data.Result.FileInfoId;
                    query.SysName = uploadFileResult.Data.Result.SysName;
                    query.DownloadUrl = uploadFileResult.Data.Result.DownloadUrl;
                    query.UpdatedUserId = request.UpdatedUserId;
                    query.UpdatedDate = DateTime.Now;

                    _context.Picture.Update(query);
                    bool status = _context.SaveChanges() > 0;
                    if (status)
                    {
                        cmdResult.Status = status;
                        cmdResult.Data = query.PictureId;
                        cmdResult.Message = "เพิ่มข้อมูลไฟล์สำเร็จ";
                    }


                    #endregion
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation("Exception is {@e}", e);
                throw e;
            }

            return await Task.FromResult(cmdResult);

        }
    }
}