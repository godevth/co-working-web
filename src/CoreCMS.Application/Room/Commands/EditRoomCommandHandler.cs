using System;
using System.Collections.Generic;
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

namespace CoreCMS.Application.Room.Commands
{
    public class EditRoomCommandHandler : BaseDbContextHandler<EditRoomCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public EditRoomCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
        public override async Task<CommandResult<bool>> Handle(EditRoomCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูลห้องได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (_context.Room.Where(o => (o.RoomName_TH == request.Name_TH || o.RoomName_EN == request.Name_EN) &&o.RoomTypeId==Convert.ToInt32(request.RoomTypeId)&& o.RoomId != request.Id &&!o.IsDeleted).Any())
                    {
                        cmdResult.Message = "ไม่สามารถเปลี่ยนข้อมูลห้องได้ เนื่องจากมีชื่อห้องนี้แล้ว";
                        cmdResult.Data = false;
                        return await Task.FromResult<CommandResult<bool>>(cmdResult);
                    }
                    var getRoom = _context.Room.Where(o => o.RoomId == request.Id).Single();

                    getRoom.RoomName_TH = request.Name_TH;
                    getRoom.RoomName_EN = request.Name_EN;
                    getRoom.Detail_TH = request.Detail_TH;
                    getRoom.Detail_EN = request.Detail_EN;
                    //getRoom.Price = Convert.ToDecimal(request.Price);
                    //getRoom.RoomQty = Convert.ToInt32(request.Qty);
                    getRoom.Capacity = Convert.ToInt32(request.Capacity);
                    getRoom.RoomTypeId = Convert.ToInt32(request.RoomTypeId);
                    getRoom.InActiveStatus = request.InActiveStatus;
                    getRoom.UpdatedUserId = request.UpdateUserId;
                    getRoom.CreatedDate = DateTime.Now;

                    getRoom.InActiveStatus = request.InActiveStatus;
                    getRoom.UpdatedUserId = request.UpdateUserId;
                    getRoom.UpdatedDate = DateTime.Now;


                    if (request.Price != null && request.Price.Count > 0)
                    {
                        List<RoomPrice> listF = new List<RoomPrice>();
                        var getItem = _context.RoomPrice.Where(o => o.RoomId == request.Id&&!o.IsDeleted).ToList();
                        var setItems = request.Price.Where(o => o.Id != null).Select(o => o.Id).ToList();
                        var add = request.Price.Where(o => o.Id == null).ToList();
                        var del = getItem.Where(o => !setItems.Contains(o.Id)).ToList();
                        var up = request.Price.Where(o => o.Id != null).ToList();
                        foreach (var item in add)
                        {
                            listF.Add(new RoomPrice()
                            {
                                Price = Convert.ToDecimal(item.Price),
                                Qty = Convert.ToInt32(item.Qty),
                                TimeType = item.TimeType,
                                CreatedUserId = request.UpdateUserId,
                                InActiveStatus = false,
                                CreatedDate = DateTime.Now
                            });
                        }

                        foreach (var item in up)
                        {
                            var imp = _context.RoomPrice.Where(o => o.Id == item.Id).Single();
                            imp.TimeType = item.TimeType;
                            imp.Qty = Convert.ToInt32(item.Qty);
                            imp.Price = Convert.ToDecimal(item.Price);
                            imp.UpdatedUserId = request.UpdateUserId;
                            imp.UpdatedDate = DateTime.Now;
                            listF.Add(imp);

                        }
                        foreach (var item in del)
                        {
                            item.IsDeleted = true;
                            item.DeletedUserId = request.UpdateUserId;
                            item.DeletedDate = DateTime.Now;
                            listF.Add(item);
                        }
                        getRoom.RoomPrice = listF;
                    }
                    else
                    {
                        var allItem = _context.RoomPrice.Where(o => o.RoomId == request.Id && !o.IsDeleted).ToList();
                        if(allItem.Count>0){
                            foreach (var item in allItem)
                        {
                            item.IsDeleted = true;
                            item.DeletedUserId = request.UpdateUserId;
                            item.DeletedDate = DateTime.Now;
                        }
                        _context.UpdateRange(allItem);
                        }
                        
                    }


                    if (request.ServiceItems != null && request.ServiceItems.Count > 0)
                    {
                        List<FacilityServices> listF = new List<FacilityServices>();
                        var getItem = _context.FacilityServices.Where(o => o.RoomId == request.Id&&!o.IsDeleted).ToList();
                        var setItems = request.ServiceItems.Where(o => o.FacilityServicesId != null).Select(o => o.FacilityServicesId).ToList();
                        var add = request.ServiceItems.Where(o => o.FacilityServicesId == null).ToList();
                        var del = getItem.Where(o => !setItems.Contains(o.FacilityServicesId)).ToList();
                        var up = request.ServiceItems.Where(o => o.FacilityServicesId != null).ToList();
                        foreach (var item in add)
                        {
                            listF.Add(new FacilityServices()
                            {
                                Price = Convert.ToDecimal(item.Price),
                                Qty = Convert.ToInt32(item.Qty),
                                FacilityId = item.FacilityId,
                                CreatedUserId = request.UpdateUserId,
                                InActiveStatus = false,
                                CreatedDate = DateTime.Now
                            });
                        }

                        foreach (var item in up)
                        {
                            var imp = _context.FacilityServices.Where(o => o.FacilityServicesId == item.FacilityServicesId).Single();
                            imp.FacilityId = item.FacilityId;
                            imp.Qty = Convert.ToInt32(item.Qty);
                            imp.Price = Convert.ToDecimal(item.Price);
                            imp.UpdatedUserId = request.UpdateUserId;
                            imp.UpdatedDate = DateTime.Now;
                            listF.Add(imp);

                        }
                        foreach (var item in del)
                        {
                            //var getDel = _context.BookingFacility.Where(o => o.FacilityId == item.FacilityId && o.Booking.RoomId == item.RoomId &&!o.Booking.BookingStatusCode.Equals("BOOKING_STATUS_CANCEL") && !o.Booking.BookingStatusCode.Equals("BOOKING_STATUS_COMPLETE")).Any();
                            //if (getDel) {
                            
                            //}
                            item.IsDeleted = true;
                            item.DeletedUserId = request.UpdateUserId;
                            item.DeletedDate = DateTime.Now;
                            listF.Add(item);
                        }
                        getRoom.FacilityServices = listF;
                    }
                    else
                    {
                        var allItem = _context.FacilityServices.Where(o => o.RoomId == request.Id && !o.IsDeleted).ToList();
                        if(allItem.Count>0){
                            foreach (var item in allItem)
                        {
                            item.IsDeleted = true;
                            item.DeletedUserId = request.UpdateUserId;
                            item.DeletedDate = DateTime.Now;
                        }
                        _context.UpdateRange(allItem);
                        }
                        
                    }

                    if (request.Pictures != null && request.Pictures.Count > 0)
                    {

                        var getItem = _context.Picture.Where(o => o.TypeRef == PictureTypeRef.Room && o.CodeRef == request.Id.ToString()&&!o.IsDeleted).ToList();
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
                                TypeRef = PictureTypeRef.Room,
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
                                TypeRef = PictureTypeRef.Room,
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
                        var allItem = _context.Picture.Where(o => o.TypeRef == PictureTypeRef.Room && o.CodeRef == request.Id.ToString()&& !o.IsDeleted).ToList();
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
                                TypeRef = PictureTypeRef.Room,
                                DeleteUserId = request.UpdateUserId,
                                FileInfoId = item.FileInfoId
                            };
                            var softDelete = await _mediator.Send(deleteCmd);
                            if (!softDelete.Status)
                                throw new Exception("ลบไฟล์เก่าไม่สำเร็จ");
                        }

                    }

                    _context.Update(getRoom);
                    var res = await _context.SaveChangesAsync();

                    cmdResult.Status = res > 0;
                    if (cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "แก้ไขข้อมูลห้องสำเร็จ";
                    }
                    trx.Commit();
                }
                catch (Exception e)
                {
                    trx.Rollback();
                    cmdResult.Message = e.Message;
                }
            }
            return await Task.FromResult<CommandResult<bool>>(cmdResult);
        }
    }
}