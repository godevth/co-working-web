using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Room.Commands
{
    public class SoftDeleteRoomCommandHandler : BaseDbContextWithMediatorHandler<SoftDeleteRoomCommand, CommandResult<bool>>
    {
        public SoftDeleteRoomCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<bool>> Handle(SoftDeleteRoomCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถลบข้อมูลได้"
            };

            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try
                {
                    var Obj = _context.Room.Where(o => o.RoomId == request.Id).Single();
                    var validPlace = _context.Booking.Where(o => o.Room.RoomId == request.Id && (!o.BookingStatusCode.Equals("BOOKING_STATUS_CANCEL") || !o.BookingStatusCode.Equals("BOOKING_STATUS_COMPLETE"))).Any();
                    if (validPlace)
                    {
                        cmdResult.Status = false;
                        cmdResult.Data = false;
                        cmdResult.Message = "ไม่สามารถลบสถานที่ " + Obj.RoomName_TH + " เนื่องจากมีการใช้งานอยู่";
                        return await Task.FromResult(cmdResult);
                    }
                    Obj.IsDeleted = true;
                    Obj.DeletedDate = DateTime.Now;
                    Obj.DeletedUserId = request.DeleteUserId;
                    
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