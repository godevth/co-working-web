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
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.Facility.Commands
{
    public class EditFacilityCommandHandler : BaseDbContextHandler<EditFacilityCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public EditFacilityCommandHandler(ApplicationDbContext context,IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
        public override async Task<CommandResult<bool>> Handle(EditFacilityCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูลประเภทสถานที่ได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                     if(_context.Facility.Where(o=>(o.FacilityName_TH == request.Name_TH||o.FacilityName_EN==request.Name_EN) && o.FacilityId != request.Id &&!o.IsDeleted).Any()){
                       cmdResult.Message = "ไม่สามารถเปลี่ยนข้อมูลประเภทสถานที่ได้ เนื่องจากมีชื่อประเภทสถานที่นี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    var getFacility = _context.Facility.Where(o=>o.FacilityId == request.Id).Single();

                        getFacility.FacilityName_TH = request.Name_TH;
                        getFacility.FacilityName_EN = request.Name_EN;
                        getFacility.FacilityTypeID =Convert.ToInt32(request.FacilityTypeId);
                        getFacility.InActiveStatus = request.InActiveStatus;
                        getFacility.UpdatedUserId = request.UpdateUserId;
                        getFacility.UpdatedDate = DateTime.Now;
                    
                    _context.Update(getFacility);
                    var res = await _context.SaveChangesAsync();

                    if (request.Pictures != null && request.Pictures.Count > 0)
                    {

                        var getItem = _context.Picture.Where(o => o.TypeRef == PictureTypeRef.Facility && o.CodeRef == request.Id.ToString()&&!o.IsDeleted).ToList();
                        var setItems = request.Pictures.Where(o => o.Id != null).Select(o => o.Id).ToList();
                        var add = request.Pictures.Where(o => o.Id == null).ToList();
                        var del = getItem.Where(o => !setItems.Contains(o.PictureId)).ToList();
                        var up = request.Pictures.Where(o => o.Id != null).ToList();
                        foreach (var item in add)
                        {
                            AddPictureCommand addPictureCmd = new AddPictureCommand()
                            {
                                FileName = Guid.NewGuid().ToString(),
                                CodeRef = request.Id.ToString(),
                                TypeRef = PictureTypeRef.Facility,
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
                                TypeRef = PictureTypeRef.Facility,
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
                        var allItem = _context.Picture.Where(o => o.TypeRef == PictureTypeRef.Facility && o.CodeRef == request.Id.ToString()&& !o.IsDeleted).ToList();
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
                                TypeRef = PictureTypeRef.Facility,
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