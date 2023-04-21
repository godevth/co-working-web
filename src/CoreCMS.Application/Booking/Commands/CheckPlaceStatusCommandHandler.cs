using System;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Room.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class CheckPlaceStatusCommandHandler : BaseDbContextWithMediatorHandler<CheckPlaceStatusCommand, PlaceStatus>
    {
        public CheckPlaceStatusCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<PlaceStatus> Handle(CheckPlaceStatusCommand request, CancellationToken cancellationToken)
        {
            PlaceStatus placeStatus = new PlaceStatus();
            #region สถานที่เปิดมั๊ย/ห้องว่างมั๊ย
            if(request.TimeType.ToLower() == "hourly")
            {
                int startTime = (request.StartDateTimeLocal.Hour * 100) + request.StartDateTimeLocal.Minute;
                int endTime = (request.EndDateTimeLocal.Hour * 100) + request.EndDateTimeLocal.Minute;

                //SearchDate วันที่นั้นๆ
                GetRangeTimeHourlyQuery hourlyQuery = new GetRangeTimeHourlyQuery()
                {
                    RoomId = request.RoomId.ToString(),
                    SearchDate = request.StartDateTime.Date,
                    BookingId = request.BookingId
                };
                var res = await _mediator.Send(hourlyQuery);
                if(res.Status)
                {
                    //ตรวจสอบเวลาเปิด
                    string[] sTimes = res.Data[0].Timestring.Split(".");
                    int sTime = (int.Parse(sTimes[0]) * 100) + int.Parse(sTimes[1]);
                    if(startTime < sTime)
                    {
                        placeStatus.IsOpen = false;
                        return await Task.FromResult(placeStatus);
                    }

                    //ตรวจสอบเวลาปิด
                    string[] eTimes = res.Data[res.Data.Count - 1].Timestring.Split(".");
                    int eTime = (int.Parse(eTimes[0]) * 100) + int.Parse(eTimes[1]);
                    if(endTime > eTime)
                    {
                        placeStatus.IsOpen = false;
                        return await Task.FromResult(placeStatus);
                    }

                    foreach(var r in res.Data)
                    {
                        string[] times = r.Timestring.Split(".");
                        int time = (int.Parse(times[0]) * 100) + int.Parse(times[1]);
                        if(time >= startTime && time <= endTime)
                        {
                            placeStatus.IsAvailable = r.IsAvailable;
                            placeStatus.IsOpen = r.IsOpen;

                            //ถ้ามีอะไร false ให้หยุด
                            if(!(placeStatus.IsAvailable && placeStatus.IsOpen))
                                break;
                        }
                    }
                }
            }
            else if(request.TimeType.ToLower() == "daily")
            {
                long startDate = ((DateTimeOffset)request.StartDateTimeLocal).ToUnixTimeMilliseconds();
                long endDate = ((DateTimeOffset)request.EndDateTimeLocal).ToUnixTimeMilliseconds();

                //init 1 of month
                bool available = true;
                DateTime dt = new DateTime(request.StartDateTimeLocal.Year, request.StartDateTimeLocal.Month, 1);
                while(dt <= request.EndDateTimeLocal)
                {
                    #region StartDate
                    GetRangeTimeDailyQuery dailyQuery = new GetRangeTimeDailyQuery()
                    {
                        RoomId = request.RoomId.ToString(),
                        Month = dt.Month,
                        Year = dt.Year,
                        BookingId = request.BookingId
                    };
                    var res = await _mediator.Send(dailyQuery);

                    if(res.Status)
                    {
                        foreach(var r in res.Data)
                        {
                            if(r.UnixMillisecond >= startDate && r.UnixMillisecond <= endDate)
                            {
                                placeStatus.IsAvailable = r.IsAvailable;
                                placeStatus.IsOpen = r.IsOpen;

                                //ถ้ามีอะไร false ให้หยุด
                                if(!(placeStatus.IsAvailable && placeStatus.IsOpen))
                                {
                                    available = false;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion

                    //เพิ่มไปทีละ 1 เดือน เพื่อตรวจสอบกรณี StartDate, EndDate ต่างเดือน
                    if(available)
                        dt = dt.AddMonths(1);
                    else
                        break;
                }
            }
            else if(request.TimeType.ToLower() == "monthly")
            {
                int startMonth = request.StartDateTimeLocal.Month;
                int endMonth = request.EndDateTimeLocal.Month;

                //init 1 of month
                bool available = true;
                DateTime dt = new DateTime(request.StartDateTimeLocal.Year, 1, 1);
                while(dt <= request.EndDateTimeLocal)
                {
                    #region StartYear
                    GetRangeTimeMonthlyQuery monthlyQuery = new GetRangeTimeMonthlyQuery()
                    {
                        RoomId = request.RoomId.ToString(),
                        Month = dt.Month,
                        Year = dt.Year,
                        BookingId = request.BookingId
                    };
                    var res = await _mediator.Send(monthlyQuery);

                    if(res.Status)
                    {
                        foreach(var r in res.Data)
                        {
                            if(r.Index >= startMonth && r.Index <= endMonth)
                            {
                                placeStatus.IsAvailable = r.IsAvailable;
                                placeStatus.IsOpen = r.IsOpen;

                                //ถ้ามีอะไร false ให้หยุด
                                if(!(placeStatus.IsAvailable && placeStatus.IsOpen))
                                {
                                    available = false;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion

                    //เพิ่มไปทีละ 1 ปี เพื่อตรวจสอบกรณี StartDate, EndDate ต่างปี
                    if(available)
                        dt = dt.AddYears(1);
                    else
                        break;
                }
            }
            #endregion
            return await Task.FromResult(placeStatus);
        }
    }
}