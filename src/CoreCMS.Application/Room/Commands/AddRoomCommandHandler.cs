using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.Room.Commands
{
    public class AddRoomCommandHandler : BaseDbContextHandler<AddRoomCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public AddRoomCommandHandler(ApplicationDbContext context,IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
        public override async Task<CommandResult<bool>> Handle(AddRoomCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถสร้างข้อมูลห้องได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                   if(_context.Room.Where(o=>(o.RoomName_TH==request.Name_TH||o.RoomName_EN==request.Name_EN)&&o.RoomTypeId==Convert.ToInt32(request.RoomTypeId)&&!o.IsDeleted).Any()){
                       cmdResult.Message = "ไม่สามารถสร้างข้อมูลห้องที่ได้ เนื่องจากมีชื่อห้องที่นี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    Domain.Room room = new Domain.Room()
                    {
                        RoomName_TH = request.Name_TH,
                        RoomName_EN = request.Name_EN,
                        PlaceId = request.PlaceId,
                        Detail_TH =request.Detail_TH,
                        Detail_EN =request.Detail_EN,
                        //Price = Convert.ToDecimal(request.Price),
                        //RoomQty = Convert.ToInt32(request.Qty),
                        Capacity = Convert.ToInt32(request.Capacity),
                        RoomTypeId = Convert.ToInt32(request.RoomTypeId),
                        InActiveStatus = request.InActiveStatus,
                        CreatedUserId = request.CreateUserId,
                        CreatedDate = DateTime.Now
                    };
                    if (request.ServiceItems != null && request.ServiceItems.Count > 0)
                    {
                        List<FacilityServices> listF = new List<FacilityServices>();
                        foreach (var item in request.ServiceItems)
                        {
                            listF.Add(new FacilityServices()
                            {
                                Price = Convert.ToDecimal(item.Price),
                                Qty = Convert.ToInt32(item.Qty),
                                FacilityId = item.FacilityId,
                                CreatedUserId = request.CreateUserId,
                                InActiveStatus = false,
                                CreatedDate = DateTime.Now
                            });
                        }
                        room.FacilityServices = listF;
                    }
                    if (request.Price != null && request.Price.Count > 0)
                    {
                        List<RoomPrice> listP = new List<RoomPrice>();
                        foreach (var item in request.Price)
                        {
                            listP.Add(new RoomPrice()
                            {
                                Price = Convert.ToDecimal(item.Price),
                                Qty = Convert.ToInt32(item.Qty),
                                TimeType = item.TimeType,
                                CreatedUserId = request.CreateUserId,
                                InActiveStatus = false,
                                CreatedDate = DateTime.Now
                            });
                        }
                        room.RoomPrice = listP;
                    }
                    _context.Add(room);
                    var res = await _context.SaveChangesAsync();

                    if (res == 0)
                        return await Task.FromResult(cmdResult);
                    #region AddPictureTH
                    if (request.Pictures!=null&&request.Pictures.Count>0)
                    {
                        foreach (var item in request.Pictures) {
                            AddPictureCommand addPictureCmd = new AddPictureCommand()
                            {
                                FileName = Guid.NewGuid().ToString(),
                                CodeRef = room.RoomId.ToString(),
                                TypeRef = PictureTypeRef.Room,
                                ContentType = item.ContentTypes,
                                FileBase64 = item.Base64s,
                                CreateUserId = request.CreateUserId
                            };
                            var picResult = await _mediator.Send(addPictureCmd);
                            int errorSize = picResult.Errors?.Count ?? 0; //picResult.Errors == null ? 0 : picResult.Errors.Count
                            if (!picResult.Status)
                            {
                                if (errorSize > 0)
                                    throw new Exception($"{string.Join(", ", picResult.Errors.Select(o => o.Value.Join(", ")))}");
                                else
                                    throw new Exception(picResult.Message);
                            }
                        }
                        
                    }
                    #endregion
                    
                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "สร้างข้อมูลห้องสำเร็จ";
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