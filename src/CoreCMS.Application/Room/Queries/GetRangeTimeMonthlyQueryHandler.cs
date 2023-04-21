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
    public class GetRangeTimeMonthlyQueryHandler : BaseDbContextHandler<GetRangeTimeMonthlyQuery, CommandResult<List<RangeTimeMonthlyModel>>>
    {

        public GetRangeTimeMonthlyQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<CommandResult<List<RangeTimeMonthlyModel>>> Handle(GetRangeTimeMonthlyQuery request, CancellationToken cancellationToken)
        {
            CommandResult<List<RangeTimeMonthlyModel>> result = new CommandResult<List<RangeTimeMonthlyModel>>();
            DateTime startMonth = new DateTime(request.Year,1,1);
            DateTime endMonth = new DateTime(request.Year + 1,1,1);
            List<RangeTimeMonthlyModel> monthList = new List<RangeTimeMonthlyModel>();

            while (startMonth < endMonth)
            {
                RangeTimeMonthlyModel item = new RangeTimeMonthlyModel()
                {   
                    Index = startMonth.Month,
                    Month = startMonth.ToString("MMMM", CultureInfo.InvariantCulture),
                    IsAvailable = false,
                    IsOpen = false,
                };
                monthList.Add(item);
                startMonth = startMonth.AddMonths(1);
            }
            monthList = monthList.OrderBy(o => o.Index).ToList();

            foreach(var month in monthList)
            {
                var booking = _context.BookingView.Where(o => o.RoomId.ToString() == request.RoomId && o.BookingStartDate.Month == month.Index && o.BookingStartDate.Year == request.Year && o.BookingStatusCode == "BOOKING_STATUS_RESERVE").ToList();
                if(booking.Count() == 0)
                {
                    month.IsAvailable = true;
                    month.IsOpen = true;
                }

                if(request.Year <= DateTime.Now.Year)
                {
                    if(month.Index <= DateTime.Now.Month)
                    {
                        month.IsAvailable = false;
                        month.IsOpen = false;
                    }
                }

                #region Check Booking
                if(request.BookingId.HasValue)
                {
                    var bookChk = booking.Where(o => o.BookingId == request.BookingId && o.BookingStartDate.Month == month.Index && o.BookingStartDate.Year == request.Year).FirstOrDefault();
                    if(bookChk != null)
                    {
                        month.IsAvailable = true;
                        month.IsOpen = true;
                    }
                    
                }
                #endregion
            }

            result.Data = monthList.ToList();
            result.Status = true;
            return await Task.FromResult<CommandResult<List<RangeTimeMonthlyModel>>>(result);
        }
    }
}