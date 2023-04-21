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
    public class GetRangeTimeHourlyQueryHandler : BaseDbContextHandler<GetRangeTimeHourlyQuery, CommandResult<List<RangeTimeHourlyModel>>>
    {

        public GetRangeTimeHourlyQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<CommandResult<List<RangeTimeHourlyModel>>> Handle(GetRangeTimeHourlyQuery request, CancellationToken cancellationToken)
        {
            CommandResult<List<RangeTimeHourlyModel>> result = new CommandResult<List<RangeTimeHourlyModel>>();
            try
            {
                
                var placeId = _context.Room.Where(o => o.RoomId.ToString() == request.RoomId).Select(o => o.PlaceId).FirstOrDefault();
                var room = _context.ImplementationDate.Where(o => o.PlaceId == placeId && !o.IsDeleted && o.StartDay == request.SearchDateLocal.DayOfWeek.ToString()).FirstOrDefault();
                if(room == null)
                {   
                    var timePlace = _context.ImplementationDate.Where(o => o.PlaceId == placeId && !o.IsDeleted).FirstOrDefault();
                    List<RangeTimeHourlyModel> timeOpenFlase = new List<RangeTimeHourlyModel>();

                   DateTime startTimeSet = DateTime.Parse(timePlace.StartTime);
                    DateTime endTimeSet = DateTime.Parse(timePlace.EndTime).AddMinutes(30);
                    while(endTimeSet > startTimeSet)
                    {
                        RangeTimeHourlyModel item = new RangeTimeHourlyModel()
                        {
                            Timestring = startTimeSet.ToString("HH.mm", CultureInfo.CurrentUICulture),
                            IsOpen = false,
                            IsAvailable = false

                        };
                        timeOpenFlase.Add(item);
                        startTimeSet = startTimeSet.AddMinutes(30);
                    }
                    result.Data = timeOpenFlase.ToList();
                    result.Status = false;
                    result.Message = "สถานที่นี้ปิดทำการ";
                    return await Task.FromResult<CommandResult<List<RangeTimeHourlyModel>>>(result);
                }
                List<RangeTimeHourlyModel> timeOpen = new List<RangeTimeHourlyModel>();

                DateTime startTime = DateTime.Parse(room.StartTime);
                DateTime endTime = DateTime.Parse(room.EndTime).AddMinutes(30);
                while(endTime > startTime)
                {
                    RangeTimeHourlyModel item = new RangeTimeHourlyModel()
                    {
                        Timestring = startTime.ToString("HH.mm", CultureInfo.CurrentUICulture),
                        IsOpen = true,
                        IsAvailable = true

                    };
                    timeOpen.Add(item);
                    startTime = startTime.AddMinutes(30);
                }
                

                var booking = _context.BookingView.Where(o => o.RoomId.ToString() == request.RoomId && 
                (o.BookingStartDate.Day == request.SearchDateLocal.Day && request.SearchDateLocal.Day == o.BookingEndDate.Day) && 
                (o.BookingStartDate.Month == request.SearchDateLocal.Month && request.SearchDateLocal.Month == o.BookingEndDate.Month) && o.BookingStatusCode == "BOOKING_STATUS_RESERVE").ToList();

                List<RangeTimeHourlyModel> listTimeBook = new List<RangeTimeHourlyModel>();
                foreach(var book in booking)
                {   
                    if(book.PriceTimeType == "Day")
                        book.BookingEndDate.AddMinutes(30);
                    while(book.BookingEndDate > book.BookingStartDate)
                    {
                        RangeTimeHourlyModel item = new RangeTimeHourlyModel()
                        {
                            Timestring = book.BookingStartDate.ToString("HH.mm", CultureInfo.CurrentUICulture)
                        };
                        listTimeBook.Add(item);
                        book.BookingStartDate = book.BookingStartDate.AddMinutes(30);
                    }
                }

                foreach(var item in listTimeBook)
                {
                    var rangTime = timeOpen.Where(o => DateTime.ParseExact(o.Timestring,"HH.mm", CultureInfo.InvariantCulture) <= DateTime.ParseExact(item.Timestring, "HH.mm", CultureInfo.InvariantCulture)).LastOrDefault();
                    if(rangTime != null)
                    {
                        rangTime.IsOpen = true;
                        rangTime.IsAvailable = false;
                    }
                }
                if(timeOpen[timeOpen.Count()-2].IsAvailable == false)
                    timeOpen[timeOpen.Count()-1].IsAvailable = false;
                
                if(DateTime.Now.Date == request.SearchDateLocal.Date)
                {
                    foreach(var item in timeOpen)
                    {
                        var timeNow = DateTime.Now.ToString("HH.mm", CultureInfo.CurrentUICulture);
                        if( DateTime.ParseExact(item.Timestring,"HH.mm",CultureInfo.InvariantCulture) < DateTime.ParseExact(timeNow,"HH.mm",CultureInfo.InvariantCulture))
                        {
                            item.IsOpen = false;
                            item.IsAvailable = false;
                        }
                    }
                }

                #region  check Booking
                if(request.BookingId.HasValue)
                {
                    List<RangeTimeHourlyModel> listTimeBookChk = new List<RangeTimeHourlyModel>();
                    
                    var bookingChk =_context.BookingView.Where( o => o.BookingId == request.BookingId 
                    && (o.BookingStartDate.Day == request.SearchDateLocal.Day && request.SearchDateLocal.Day == o.BookingEndDate.Day) 
                    && (o.BookingStartDate.Month == request.SearchDateLocal.Month && request.SearchDateLocal.Month == o.BookingEndDate.Month) 
                    && o.BookingStatusCode == "BOOKING_STATUS_RESERVE").FirstOrDefault();

                    while(bookingChk.BookingEndDate >= bookingChk.BookingStartDate)
                    {
                        RangeTimeHourlyModel item = new RangeTimeHourlyModel()
                        {
                            Timestring = bookingChk.BookingStartDate.ToString("HH.mm", CultureInfo.CurrentUICulture)
                        };
                        listTimeBookChk.Add(item);
                        bookingChk.BookingStartDate = bookingChk.BookingStartDate.AddMinutes(30);
                    }

                    foreach(var item in listTimeBookChk)
                    {
                        var rangTime = timeOpen.Where(o => DateTime.ParseExact(o.Timestring,"HH.mm", CultureInfo.InvariantCulture) <= DateTime.ParseExact(item.Timestring, "HH.mm", CultureInfo.InvariantCulture)).LastOrDefault();
                        if(rangTime != null)
                        {
                            rangTime.IsAvailable = true;
                        }
                    }
                }
                #endregion
                result.Data = timeOpen.ToList();
                result.Status = true;
                
            }
            catch (Exception e)
            {
                throw e;
            }

            return await Task.FromResult<CommandResult<List<RangeTimeHourlyModel>>>(result);

        }
    }
}