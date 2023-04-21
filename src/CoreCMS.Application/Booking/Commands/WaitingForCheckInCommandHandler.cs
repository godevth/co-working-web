using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.Booking.Commands
{
    public class WaitingForCheckInCommandHandler : BaseDbContextWithMediatorHandler<WaitingForCheckInCommand, CommandResult<int>>
    {
        private readonly CowConfig _config;

        public WaitingForCheckInCommandHandler(ApplicationDbContext context, IMediator mediator, IOptions<CowConfig> config) : base(context, mediator)
        {
            _config = config.Value;
        }

        public override async Task<CommandResult<int>> Handle(WaitingForCheckInCommand request, CancellationToken cancellationToken)
        {
            CommandResult<int> cmdResult = new CommandResult<int>()
            {
                Message = "ไม่พบข้อมูลให้เปลี่ยนสถานะ (รอ Check In)"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    string[] status = new string[]{ "BOOKING_STATUS_PAID", "BOOKING_STATUS_CHECKIN" };
                    var bookingDates = _context.BookingDate
                            .Where(o => status.Contains(o.Booking.BookingStatusCode) && !o.IsCheckIn 
                                && o.StartDate.Date == DateTime.Today.Date)
                            .ToList();

                    if(bookingDates == null || !bookingDates.Any())
                    {
                        return await Task.FromResult(cmdResult);
                    }
                    
                    int rows = 0;
                    DateTime dt = DateTime.Now; 
                    foreach(var bd in bookingDates)
                    {
                        TimeSpan diff = dt.Subtract(bd.StartDate);
                        bool b = Math.Abs(diff.TotalMinutes) <= _config.CheckInTime;

                        if(!b)
                            continue;
                        
                        var bookingUpd = _context.Booking.Where(o => o.BookingId == bd.BookingId).SingleOrDefault();
                        bookingUpd.BookingStatusCode = "BOOKING_STATUS_WAITING_FOR_CHECKIN";
                        bookingUpd.UpdatedDate = DateTime.Now;
                        bookingUpd.UpdatedUserId = _config.BatchUserId;
                        _context.Booking.Update(bookingUpd);

                        rows += _context.SaveChanges();
                    }

                    if (rows > 0)
                    {
                        if (!nestedTrans)
                            ts.Commit();
                            
                        cmdResult.Data = bookingDates.Count;
                        cmdResult.Message = "เปลี่ยนสถานะ (รอ Check In) สำเร็จ";
                        cmdResult.Status = true;
                    }
                }
                catch (Exception e)
                {
                    if (!nestedTrans)
                        ts.Rollback();

                    cmdResult.Message = e.Message;
                }
            }
            
            return await Task.FromResult(cmdResult);
        }
    }
}