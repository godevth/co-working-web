using System;
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

namespace CoreCMS.Application.RoomType.Commands
{
    public class AddRoomTypeCommandHandler : BaseDbContextHandler<AddRoomTypeCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public AddRoomTypeCommandHandler(ApplicationDbContext context,IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
        public override async Task<CommandResult<bool>> Handle(AddRoomTypeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถสร้างข้อมูลประเภทสถานที่ได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                   if(_context.RoomType.Where(o=>o.RoomTypeName==request.Name&&!o.IsDeleted).Any()){
                       cmdResult.Message = "ไม่สามารถสร้างข้อมูลประเภทสถานที่ได้ เนื่องจากมีชื่อประเภทสถานที่นี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    Domain.RoomType roomType = new Domain.RoomType()
                    {
                        RoomTypeName = request.Name,
                        RoomTypeNameEN = request.NameEN,
                        InActiveStatus = request.InActiveStatus,
                        CreatedUserId = request.CreateUserId,
                        CreatedDate = DateTime.Now
                    };
                    
                    _context.Add(roomType);
                    var res = await _context.SaveChangesAsync();

                    #region AddPicture
                    if (request.Picture !=null)
                    {
                        AddPictureCommand addPictureCmd = new AddPictureCommand()
                        {
                            FileName = Guid.NewGuid().ToString(),
                            CodeRef = roomType.RoomTypeID.ToString(),
                            TypeRef = PictureTypeRef.RoomType,
                            ContentType = request.Picture.ContentTypes,
                            FileBase64 = request.Picture.Base64s,
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
                    #endregion

                
                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "สร้างข้อมูลประเภทสถานที่สำเร็จ";
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