using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;
using CoreCMS.Application.Booking.Commands;
using CoreCMS.Data;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.Booking.Queries
{
    public class SearchBookingQueryHandler : BaseDbContextWithMediatorHandler<SearchBookingQuery, SearchResult<HistoryView>>
    {
        private readonly CowConfig _config;
        public SearchBookingQueryHandler(ApplicationDbContext context, IMediator mediator, IOptions<CowConfig> config) : base(context, mediator)
        {
            _config = config.Value;
        }

        public override async Task<SearchResult<HistoryView>> Handle(SearchBookingQuery request, CancellationToken cancellationToken)
        {
            SearchResult<HistoryView> result = new SearchResult<HistoryView>();

            var query = _context.HistoryView
                .Where(o => o.IsDeleted == request.IsDeleted)
                .Select(o => new HistoryView()
                {
                    BookingId = o.BookingId,
                    BookingNumber = o.BookingNumber,
                    RoomId = o.RoomId,
                    RoomNameTH = o.RoomNameTH,
                    RoomNameEN = o.RoomNameEN,
                    RoomCapacity = o.RoomCapacity,
                    PlaceId = o.PlaceId,
                    PlaceNameTH = o.PlaceNameTH,
                    PlaceNameEN = o.PlaceNameEN,
                    PriceTimeType = o.PriceTimeType,
                    PriceQty = o.PriceQty,
                    Price = o.Price,
                    BookingStartDate = o.BookingStartDate,
                    BookingEndDate = o.BookingEndDate,
                    BookingStatusCode = o.BookingStatusCode,
                    BookingStatusName = o.VoidCode == "VOID_TYPE_REFUNDED" ? 
                        $"{o.BookingStatus.SystemVariableName} ({o.Void.SystemVariableName})" : o.BookingStatus.SystemVariableName,
                    PaymentMethodCode = o.PaymentMethodCode,
                    PaymentMethodName = o.PaymentMethod.SystemVariableName,
                    Qty = o.Qty,
                    IsOwner = o.IsOwner,
                    CustomerEmail = o.IsOwner ? o.OwnerEmail : o.CustomerEmail,
                    CustomerFirstname = o.IsOwner ? o.OwnerFirstName : o.CustomerFirstname,
                    CustomerLastname = o.IsOwner ? o.OwnerLastName: o.CustomerLastname,
                    CustomerPhoneNumber = o.IsOwner ? o.OwnerPhoneNumber : o.CustomerPhoneNumber,
                    Total = o.Total,
                    Tax = o.Tax,
                    Discount = o.Discount,
                    GrandTotal = o.GrandTotal,
                    Remark = o.Remark,
                    InActiveStatus = o.InActiveStatus,
                    IsRefund = o.VoidCode == "VOID_TYPE_IN_PROGRESS"
                });

            #region filter SearchKeyword
            if (!string.IsNullOrEmpty(request.SearchKeyword))
            {
                string keyword = request.SearchKeyword.Trim().ToLower();
                query = query.Where(o =>
                        o.CustomerEmail.ToLower().Contains(keyword)
                        || o.CustomerFirstname.ToLower().Contains(keyword)
                        || o.CustomerLastname.ToLower().Contains(keyword)
                        || o.CustomerPhoneNumber.ToLower().Contains(keyword)
                        || o.BookingNumber.ToLower().Contains(keyword)
                        || o.RoomNameTH.ToLower().Contains(keyword)
                        || o.RoomNameEN.ToLower().Contains(keyword)
                        || o.PlaceNameTH.ToLower().Contains(keyword)
                        || o.PlaceNameEN.ToLower().Contains(keyword));
            }
            #endregion

            bool hasRole = request.IsSuperAdmin;
            bool chk = hasRole;
            #region Check Roles Owner Place
            if(!chk)
            {
                var places = _context.CompanyProfiles
                    .Where(o => !o.IsDeleted && !o.InActiveStatus && o.Company.OwnerId == request.UserId)
                    .Select(o => o.PlaceId)
                    .ToList();
                if(places.Any())
                {
                    #region PlaceId
                    if (request.PlaceId.HasValue)
                    {
                        if(!places.Where(o => o == request.PlaceId.Value).Any())
                            chk = false;
                        else
                        {
                            query = query.Where(o => o.PlaceId == request.PlaceId.Value);
                            chk = true;
                        }
                    }
                    else
                    {
                        query = query.Where(o => places.Contains(o.PlaceId));
                        chk = true;
                    }
                    #endregion
                    
                }
            }
            #endregion

            #region Check Roles Admin Place
            if(!chk)
            {
                var places = _context.CompanyProfiles
                    .Where(o => !o.IsDeleted && !o.InActiveStatus && o.AdminId == request.UserId)
                    .Select(o => o.PlaceId)
                    .ToList();
                if(places.Any())
                {
                    #region PlaceId
                    if (request.PlaceId.HasValue)
                    {
                        if(!places.Where(o => o == request.PlaceId.Value).Any())
                            chk = false;
                        else
                        {
                            query = query.Where(o => o.PlaceId == request.PlaceId.Value);
                            chk = true;
                        }
                    }
                    else
                    {
                        query = query.Where(o => places.Contains(o.PlaceId));
                        chk = true;
                    }
                    #endregion
                }
            }
            #endregion

            if(!chk)
                throw new Exception("Can not Permission");

            if(request.IsSuperAdmin)
            {
                #region PlaceId
                if (request.PlaceId.HasValue)
                {
                    query = query.Where(o => o.PlaceId == request.PlaceId.Value);
                }
                #endregion
            }

            #region RoomId
            if (request.RoomId.HasValue)
            {
                query = query.Where(o => o.RoomId == request.RoomId.Value);
            }
            #endregion

            #region StartDate & EndDate
            if (request.StartDateTime.HasValue)
            {
                query = query.Where(o => o.BookingStartDate >= request.StartDateTime.Value);
            }
            if (request.EndDateTime.HasValue)
            {
                query = query.Where(o => o.BookingEndDate <= request.EndDateTime.Value);
            }
            #endregion

            #region InActiveStatus
            query = query.Where(o => o.InActiveStatus == request.InActiveStatus);
            #endregion

            StringBuilder sortBd = new StringBuilder();
            for (int i = 0; i < request.SortBy.Length; i++)
            {
                var col = request.SortBy[i];
                var desc = request.SortDesc[i];

                if(col.ToLower() == nameof(HistoryView.BookingStartDateString).ToLower())
                    col = nameof(HistoryView.BookingStartDate);
                else if(col.ToLower() == nameof(HistoryView.BookingEndDateString).ToLower())
                    col = nameof(HistoryView.BookingEndDate);
                else if(col.ToLower() == nameof(HistoryView.CustomerName).ToLower())
                    col = nameof(HistoryView.CustomerFirstname);

                if (desc)
                {
                    sortBd.Append($"{col} DESC,");
                }
                else
                {
                    sortBd.Append($"{col},");
                }
            }

            if (sortBd.Length > 0)
            {
                sortBd.Length--;
                query = query.OrderBy(sortBd.ToString());
            }

            int skip = (request.Page - 1) * request.ItemsPerPage;

            result.Total = await query.CountAsync();
            result.Items = await query.Skip(skip).Take(request.ItemsPerPage).ToArrayAsync();
            
            #region Set CheckIn & CheckOut
            if(result.Total > 0)
            {
                foreach(var bv in result.Items)
                {   
                    DateTime dt = DateTime.Now;
                    TimeSpan diffCancel = bv.BookingStartDate.Subtract(dt);
                    bool bCancel = diffCancel.TotalMinutes >= _config.CancelBookingTime; 
                    bv.IsCancel = bCancel && (bv.BookingStatusCode == "BOOKING_STATUS_RESERVE" || bv.BookingStatusCode == "BOOKING_STATUS_PAID");
                    
                    string[] status = new string[]{ "BOOKING_STATUS_CANCEL", "BOOKING_STATUS_PLACE_CANCEL" };
                    if(status.Contains(bv.BookingStatusCode))
                        continue;

                    var bookingDate = _context.BookingDate
                    .Where(o => o.BookingId == bv.BookingId 
                        && o.StartDate.Date == DateTime.Today.Date && o.EndDate.Date == DateTime.Today.Date)
                    .FirstOrDefault();
                    if(bookingDate == null)
                        continue;

                    TimeSpan diffCheckIn = dt.Subtract(bookingDate.StartDate);
                    bool bCheckIn = Math.Abs(diffCheckIn.TotalMinutes) <= _config.CheckInTime;

                    if(bCheckIn)
                    {
                        if(!bookingDate.IsCheckIn)
                            bv.IsCheckIn = true;
                    }

                    if(bookingDate.IsCheckIn && !bookingDate.IsCheckOut)
                        bv.IsCheckOut = true;
                }
            }
            #endregion

            return result;
        }
    }
}