using CoreCMS.Application.Shared;
using CoreCMS.Persistence;
using MediatR;
using CoreCMS.Application.Room.Models;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CoreCMS.Application.Shared.Model;

namespace CoreCMS.Application.Room.Queries
{
    public class GetRangeTimeDailyQueryHandler : BaseDbContextHandler<GetRangeTimeDailyQuery, CommandResult<List<RangeTimeDailyModel>>>
    {

        public GetRangeTimeDailyQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<CommandResult<List<RangeTimeDailyModel>>> Handle(GetRangeTimeDailyQuery request, CancellationToken cancellationToken)
        {
            CommandResult<List<RangeTimeDailyModel>> result = new CommandResult<List<RangeTimeDailyModel>>();

            int days = DateTime.DaysInMonth(request.Year, request.Month) + 1 ;
            int start = 1;
            List<RangeTimeDailyModel> dayList = new List<RangeTimeDailyModel>();

            while (start < days)
            {
                RangeTimeDailyModel item = new RangeTimeDailyModel()
                {   
                    Index = start,
                    Day = new DateTime(request.Year, request.Month, start),
                    IsAvailable = true,
                    IsOpen = false,
                };
                dayList.Add(item);
                start++;
            }

            var placeId = _context.Room.Where(o => o.RoomId.ToString() == request.RoomId).Select(o => o.PlaceId).FirstOrDefault();
            var room = _context.ImplementationDate.Where(o => o.PlaceId == placeId && !o.IsDeleted ).Select( o => o.StartDay).ToList();

            var booKing = _context.BookingView.Where(o => o.RoomId.ToString() == request.RoomId &&  o.BookingStartDate.Month == request.Month 
            && o.BookingStartDate.Year == request.Year && o.BookingStatusCode == "BOOKING_STATUS_RESERVE").OrderBy(o => o.BookingStartDate).ToList();

            foreach(var day in dayList)
            {
                day.Month = day.Day.ToString("MMMM", CultureInfo.InvariantCulture);
                TimeZoneInfo timezone = TimeZoneInfo.Local; 
                DateTimeOffset timeOffset = TimeZoneInfo.ConvertTime(DateTimeOffset.Parse(day.Day.ToString()), timezone);
                day.UnixMillisecond = timeOffset.ToUnixTimeMilliseconds();

                if(room.Contains(day.Day.DayOfWeek.ToString()))
                {
                    day.IsOpen = true;
                }
                
                var item = booKing.Where(o => o.BookingStartDate.Day == day.Day.Day).FirstOrDefault();
                if(item != null)
                    day.IsAvailable = false;

                if(day.IsOpen == false)
                    day.IsAvailable = false;

                if(day.Day.Month <= DateTime.Now.Month)
                {
                    if(day.Day.Date < DateTime.Now.Date)
                    {
                        day.IsOpen = false;
                        day.IsAvailable = false;
                    }
                }
                #region Check Booking
                if(request.BookingId.HasValue)
                {
                    var bookChk = booKing.Where(o => o.BookingStartDate.Day == day.Day.Day && o.BookingId == request.BookingId).FirstOrDefault();
                    if(bookChk != null)
                    {
                        day.IsAvailable = true;
                        day.IsOpen = true;
                    }
                    
                }
                #endregion
            }
           

            result.Data = dayList.ToList();
            result.Status = true;

            return await Task.FromResult<CommandResult<List<RangeTimeDailyModel>>>(result);
        }
    }
}