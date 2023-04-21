using System;
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
    public class CheckOutAllCommandHandler : BaseDbContextWithMediatorHandler<CheckOutAllCommand, CommandResult<int>>
    {
        private readonly CowConfig _config;

        public CheckOutAllCommandHandler(ApplicationDbContext context, IMediator mediator, IOptions<CowConfig> config) : base(context, mediator)
        {
            _config = config.Value;
        }

        public override async Task<CommandResult<int>> Handle(CheckOutAllCommand request, CancellationToken cancellationToken)
        {
            CommandResult<int> cmdResult = new CommandResult<int>()
            {
                Message = "ไม่พบข้อมูลให้เช็คเอาต์"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    DateTime dt = DateTime.Now; 
                    var bookingDates = _context.BookingDate
                            .Where(o => o.EndDate < dt && o.IsCheckIn && !o.IsCheckOut)
                            .ToList();

                    if(bookingDates == null || !bookingDates.Any())
                    {
                        return await Task.FromResult(cmdResult);
                    }
                    
                    int rows = 0;
                    foreach(var bd in bookingDates)
                    {
                        TimeSpan diff = dt.Subtract(bd.EndDate);
                        bool b = Math.Abs(diff.TotalMinutes) >= _config.CheckOutTime;

                        if(!b)
                            continue;
                        
                        var checkIn = _context.CheckIn
                        .Where(o => !o.IsDeleted && !o.InActiveStatus && o.BookingId == bd.BookingId)
                        .OrderByDescending(o => o.CheckInDate)
                        .FirstOrDefault();

                        if(checkIn != null)
                        {
                            #region CheckOut
                            checkIn.CheckOutUserId = _config.BatchUserId;
                            checkIn.CheckOutDate = DateTime.Now;
                            checkIn.UpdatedUserId =  _config.BatchUserId;
                            checkIn.UpdatedDate = checkIn.CheckOutDate;
                            _context.CheckIn.Update(checkIn);
                            #endregion

                            #region Update Booking Date
                            bd.IsCheckOut = true;
                            _context.BookingDate.Update(bd);
                            #endregion

                            #region Update Booking Status
                            var isBookingDate = _context.BookingDate
                                    .Where(o => o.BookingId == bd.BookingId 
                                        && o.StartDate.Date > DateTime.Today.Date)
                                    .Any();
                            if(!isBookingDate)
                            {
                                var bookingUpd = _context.Booking.Where(o => o.BookingId == bd.BookingId).SingleOrDefault();
                                bookingUpd.BookingStatusCode = "BOOKING_STATUS_COMPLETE";
                                bookingUpd.UpdatedDate = DateTime.Now;
                                bookingUpd.UpdatedUserId = _config.BatchUserId;
                                _context.Booking.Update(bookingUpd);
                            }
                            #endregion

                            rows += _context.SaveChanges();
                        }
                    }

                    if (rows > 0)
                    {
                        if (!nestedTrans)
                            ts.Commit();
                            
                        cmdResult.Message = "เช็คเอาต์สำเร็จ";
                        cmdResult.Data = bookingDates.Count;
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