using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterFile.Commands;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Picture.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class SoftDeleteUserCommandHandler : BaseDbContextWithMediatorHandler<SoftDeleteUserCommand, CommandResult<bool>>
    {
        public SoftDeleteUserCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<bool>> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถลบข้อมูลได้"
            };

            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try
                {
                    var user = _context.Users.Where(o => o.Id == request.UserId).Single();
                    
                    #region DeletePicture
                    GetPictureByCodeTypeQuery getPicture = new GetPictureByCodeTypeQuery()
                    {
                        CodeRef = user.Id,
                        TypeRef = PictureTypeRef.User
                    };
                    var picture = await _mediator.Send(getPicture);
                    if(picture != null)
                    {
                        DeleteFileCommand deleteFileCmd = new DeleteFileCommand()
                        {
                            FileInfoId = picture.FileInfoId
                        };
                        var deleteFileRes = await _mediator.Send(deleteFileCmd);
                        if(!deleteFileRes)
                            throw new Exception("ลบไฟล์ไม่สำเร็จ");
                    }

                    SoftDeletePictureCommand softDeleteCmd = new SoftDeletePictureCommand()
                    {
                        CodeRef = user.Id,
                        TypeRef = PictureTypeRef.User,
                        DeleteUserId = request.DeleteUserId
                    };
                    var softDelete = await _mediator.Send(softDeleteCmd);
                    if(!softDelete.Status)
                        throw new Exception("ลบไฟล์ไม่สำเร็จ");
                    #endregion

                    user.IsDeleted = true;
                    user.DeletedDate = DateTime.Now;
                    user.DeletedUserId = request.DeleteUserId;
                    var status = _context.SaveChanges();
                    if(status > 0 )
                    {
                        cmdResult.Message = "ลบข้อมูลสำเร็จ";
                        cmdResult.Status = true;
                        cmdResult.Data = true;
                    }
                    trx.Commit ();
                }
                catch (Exception e) {
                    trx.Rollback ();
                    cmdResult.Message = e.Message;
                }
            }
            return await Task.FromResult(cmdResult);
        }
    }
}