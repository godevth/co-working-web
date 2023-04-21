using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterFile.Commands;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.RoomType.Commands
{
    public class EditRoomTypeCommandHandler : BaseDbContextHandler<EditRoomTypeCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public EditRoomTypeCommandHandler(ApplicationDbContext context,IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
        public override async Task<CommandResult<bool>> Handle(EditRoomTypeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูลประเภทสถานที่ได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                     if(_context.RoomType.Where(o=>o.RoomTypeName==request.Name &&o.RoomTypeID!=request.Id &&!o.IsDeleted).Any()){
                       cmdResult.Message = "ไม่สามารถเปลี่ยนข้อมูลประเภทสถานที่ได้ เนื่องจากมีชื่อประเภทสถานที่นี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    var getLocation = _context.RoomType.Where(o=>o.RoomTypeID==request.Id).Single();

                        getLocation.RoomTypeName = request.Name;
                        getLocation.RoomTypeNameEN = request.NameEN;
                        getLocation.InActiveStatus = request.InActiveStatus;
                        getLocation.UpdatedUserId = request.UpdateUserId;
                        getLocation.UpdatedDate = DateTime.Now;
                    
                    _context.Update(getLocation);
                    var res = await _context.SaveChangesAsync();

                    if (request.Picture != null && request.Picture.Count > 0)
                    {

                        var getItem = _context.Picture.Where(o => o.TypeRef == PictureTypeRef.RoomType && o.CodeRef == request.Id.ToString()&&!o.IsDeleted).ToList();
                        var setItems = request.Picture.Where(o => o.Id != null).Select(o => o.Id).ToList();
                        var add = request.Picture.Where(o => o.Id == null).ToList();
                        var del = getItem.Where(o => !setItems.Contains(o.PictureId)).ToList();
                        var up = request.Picture.Where(o => o.Id != null).ToList();
                        foreach (var item in add)
                        {
                            AddPictureCommand addPictureCmd = new AddPictureCommand()
                            {
                                FileName = Guid.NewGuid().ToString(),
                                CodeRef = request.Id.ToString(),
                                TypeRef = PictureTypeRef.RoomType,
                                ContentType = item.ContentTypes,
                                FileBase64 = item.Base64s,
                                CreateUserId = request.UpdateUserId
                            };
                            var picResult = await _mediator.Send(addPictureCmd);
                        }

                        foreach (var item in del)
                        {
                            DeleteFileCommand deleteFileCmd = new DeleteFileCommand()
                            {
                                FileInfoId = item.FileInfoId
                            };
                            var deleteFileRes = await _mediator.Send(deleteFileCmd);
                            if (!deleteFileRes)
                                throw new Exception("ลบไฟล์เก่าไม่สำเร็จ");

                            SoftDeletePictureCommand deleteCmd = new SoftDeletePictureCommand()
                            {
                                CodeRef = request.Id.ToString(),
                                TypeRef = PictureTypeRef.RoomType,
                                DeleteUserId = request.UpdateUserId,
                                FileInfoId = item.FileInfoId
                            };
                            var softDelete = await _mediator.Send(deleteCmd);
                            if (!softDelete.Status)
                                throw new Exception("ลบไฟล์เก่าไม่สำเร็จ");
                        }
                    }
                    else
                    {
                        var allItem = _context.Picture.Where(o => o.TypeRef == PictureTypeRef.RoomType && o.CodeRef == request.Id.ToString()&& !o.IsDeleted).ToList();
                        foreach (var item in allItem)
                        {
                           DeleteFileCommand deleteFileCmd = new DeleteFileCommand()
                            {
                                FileInfoId = item.FileInfoId
                            };
                            var deleteFileRes = await _mediator.Send(deleteFileCmd);
                            if (!deleteFileRes)
                                throw new Exception("ลบไฟล์เก่าไม่สำเร็จ");

                            SoftDeletePictureCommand deleteCmd = new SoftDeletePictureCommand()
                            {
                                CodeRef = request.Id.ToString(),
                                TypeRef = PictureTypeRef.RoomType,
                                DeleteUserId = request.UpdateUserId,
                                FileInfoId = item.FileInfoId
                            };
                            var softDelete = await _mediator.Send(deleteCmd);
                            if (!softDelete.Status)
                                throw new Exception("ลบไฟล์เก่าไม่สำเร็จ");
                        }
                    }
                    
                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "แก้ไขข้อมูลประเภทสถานที่สำเร็จ";
                    }
                    trx.Commit ();
                }
                catch (Exception e) {
                    trx.Rollback ();
                    cmdResult.Message = e.Message;
                }
            }
          return await Task.FromResult<CommandResult<bool>>(cmdResult);
        }
    }
}