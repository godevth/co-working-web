using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Booking.Queries
{
    public class HistoryQueryHandler : BaseDbContextHandler<HistoryQuery, SearchResult<HistoryView>>
    {
        private readonly CowConfig _config;
        public HistoryQueryHandler(ApplicationDbContext context, IOptions<CowConfig> config) : base(context)
        {
            _config = config.Value;
        }
        public override async Task<SearchResult<HistoryView>> Handle(HistoryQuery request, CancellationToken cancellationToken)
        {
            SearchResult<HistoryView> result = new SearchResult<HistoryView>();
            try
            {
                if (request.ItemsPerPage == 0)
                {
                    request.ItemsPerPage = 25;
                }

                var getQuery = _context.HistoryView.Where(o => o.OwnerId == request.UserId || o.CustomerId == request.UserId);

            #region filter SearchKeyword
            if (!string.IsNullOrEmpty(request.Search))
            {
                string keyword = request.Search.Trim().ToLower();
                getQuery = getQuery.Where(o =>
                        o.PlaceNameTH.ToLower().Contains(keyword)
                        || o.PlaceNameEN.ToLower().Contains(keyword));
            }
            #endregion

                if (request.StartDate != null&&!request.StartDate.Equals(""))
                {
                    DateTime StartDateValue = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    getQuery=getQuery.Where(o => o.BookingStartDate >= StartDateValue);
                }
                if (request.EndDate != null&&!request.EndDate.Equals(""))
                {
                    DateTime EndDateValue = DateTime.ParseExact(request.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    EndDateValue = EndDateValue.AddDays(1).AddMilliseconds(-1);
                    getQuery=getQuery.Where(o => o.BookingEndDate <= EndDateValue);
                }

                //if (request.GetStatus == "booking")
                //{
                //    getQuery = getQuery.Where(o => !o.BookingStatusCode.Equals("BOOKING_STATUS_CANCEL") || !o.BookingStatusCode.Equals("BOOKING_STATUS_COMPLETE"));
                //}
                //else 
                if (request.GetStatus.ToLower().Equals("cancel"))
                {
                    getQuery = getQuery.Where(o => o.BookingStatusCode.Equals("BOOKING_STATUS_CANCEL") || o.BookingStatusCode.Equals("BOOKING_STATUS_PLACE_CANCEL"));
                }
                else if(request.GetStatus.ToLower().Equals("all"))
                {
                    getQuery = getQuery.Where(o => o.BookingStatusCode.Equals("BOOKING_STATUS_CANCEL") || o.BookingStatusCode.Equals("BOOKING_STATUS_COMPLETE") || o.BookingStatusCode.Equals("BOOKING_STATUS_PLACE_CANCEL"));
                }else{
                    getQuery = getQuery.Where(o => !o.BookingStatusCode.Equals("BOOKING_STATUS_CANCEL") || !o.BookingStatusCode.Equals("BOOKING_STATUS_COMPLETE") || !o.BookingStatusCode.Equals("BOOKING_STATUS_PLACE_CANCEL"));
                }

                var bookingStatusColorDict = _config.BookingStatusColorDict;
                var bookingStatusIconDict = _config.BookingStatusIconDict;

                var query = getQuery
               .Select(o => new HistoryView()
               {
                   BookingEndDate = o.BookingEndDate,
                   BookingId = o.BookingId,
                   BookingNumber = o.BookingNumber,
                   BookingRunning = o.BookingRunning,
                   BookingStartDate = o.BookingStartDate,
                   BookingStatusCode = o.BookingStatusCode,
                   BookingStatusName = o.SystemVariableName,
                   CreatedDate = o.CreatedDate,
                   CustomerEmail = o.CustomerEmail,
                   CustomerId = o.CustomerId,
                   CustomerFirstname = o.CustomerFirstname,
                   CustomerLastname = o.CustomerLastname,
                   CustomerPhoneNumber = o.CustomerPhoneNumber,
                   Discount = o.Discount,
                   GrandTotal = o.GrandTotal,
                   InActiveStatus = o.InActiveStatus,
                   IsDeleted = o.IsDeleted,
                   OwnerEmail = o.OwnerEmail,
                   IsOwner = o.IsOwner,
                   OwnerFirstName = o.OwnerFirstName,
                   OwnerId = o.OwnerId,
                   OwnerLastName = o.OwnerLastName,
                   OwnerPhoneCountryCode = o.OwnerPhoneCountryCode,
                   OwnerPhoneNumber = o.OwnerPhoneNumber,
                   PaymentMethodCode = o.PaymentMethodCode,
                   PaymentMethodName = o.PaymentMethodName,
                   PlaceAddress = o.PlaceAddress,
                   PlaceAmperId = o.PlaceAmperId,
                   PlaceDetail = o.PlaceDetail,
                   PlaceId = o.PlaceId,
                   PlaceLatitude = o.PlaceLatitude,
                   PlaceLongitude = o.PlaceLongitude,
                   PlaceNameEN = request.Language=="TH"?o.PlaceNameTH:o.PlaceNameEN,
                   PlaceNameTH = request.Language=="TH"?o.PlaceNameTH:o.PlaceNameEN,
                   PlaceNearBy = o.PlaceNearBy,
                   PlaceProvinceId = o.PlaceProvinceId,
                   PlaceTambonId = o.PlaceTambonId,
                   PlaceTypeId = o.PlaceTypeId,
                   PlaceTypeName = request.Language == "EN" ? o.PlaceTypeNameEN != null ? o.PlaceTypeNameEN : o.PlaceTypeName : o.PlaceTypeName,
                   PlaceZipCode = o.PlaceZipCode,
                   Price = o.Price,
                   PriceQty = o.PriceQty,
                   PriceTimeType = o.PriceTimeType,
                   Qty = o.Qty,
                   Remark = o.Remark,
                   RoomCapacity = o.RoomCapacity,
                   RoomId = o.RoomId,
                   RoomNameEN = request.Language=="TH"?o.RoomNameTH:o.RoomNameEN,
                   RoomNameTH = request.Language=="TH"?o.RoomNameTH:o.RoomNameEN,
                   RoomTypeId = o.RoomTypeId,
                   RoomTypeName = request.Language == "EN" ? o.RoomNameEN != null ? o.RoomNameEN : o.RoomNameTH : o.RoomNameTH,
                   Tax = o.Tax,
                   Total = o.Total,
                   BookingStatusColor = bookingStatusColorDict[o.BookingStatusCode],
                   BookingStatusIconName = bookingStatusIconDict[o.BookingStatusCode]
               }
                    ).Where(o => !o.IsDeleted).OrderBy(o => o.BookingStartDate);
                int skip = request.Page * request.ItemsPerPage;
                //result.Total = await query.CountAsync();
                 result.Items = await query.ToArrayAsync();
                if (query.Count() > 0)
                {
                    foreach (var bv in result.Items)
                    {
                        DateTime dt = DateTime.Now;
                        TimeSpan diffCancel = bv.BookingStartDate.Subtract(dt);
                        bool bCancel = diffCancel.TotalMinutes >= _config.CancelBookingTime;
                        bv.IsCancel = bCancel && (bv.BookingStatusCode == "BOOKING_STATUS_RESERVE" || bv.BookingStatusCode == "BOOKING_STATUS_PAID");

                        if (bv.BookingStatusCode == "BOOKING_STATUS_CANCEL" || bv.BookingStatusCode == "BOOKING_STATUS_PLACE_CANCEL")
                            continue;

                        var bookingDate = _context.BookingDate
                        .Where(o => o.BookingId == bv.BookingId
                            && o.StartDate.Date == DateTime.Today.Date && o.EndDate.Date == DateTime.Today.Date)
                        .FirstOrDefault();
                        if (bookingDate == null)
                            continue;

                        TimeSpan diffCheckIn = dt.Subtract(bookingDate.StartDate);
                        bool bCheckIn = Math.Abs(diffCheckIn.TotalMinutes) <= _config.CheckInTime;

                        if (bCheckIn)
                        {
                            if (!bookingDate.IsCheckIn)
                                bv.IsCheckIn = true;
                        }

                        if (bookingDate.IsCheckIn && !bookingDate.IsCheckOut)
                            bv.IsCheckOut = true;
                    }

                }
                if (request.GetStatus.ToLower().Equals("coming"))
                {
                    result.Items = result.Items.Where(o => o.IsCancel).Skip(skip).Take(request.ItemsPerPage).ToArray();
                }
                else if (request.GetStatus.ToLower().Equals("checkinout"))
                {
                    result.Items = result.Items.Where(o => o.IsCheckIn || o.IsCheckOut).Skip(skip).Take(request.ItemsPerPage).ToArray();
                }
                else {
                    result.Items = result.Items.Skip(skip).Take(request.ItemsPerPage).ToArray();
                }
                result.Total = result.Items.Count();
                

                
                return await Task.FromResult<SearchResult<HistoryView>>(result);
            }
            catch (Exception ex)
            {
                return await Task.FromResult<SearchResult<HistoryView>>(result);
            }
        }
    }
}
