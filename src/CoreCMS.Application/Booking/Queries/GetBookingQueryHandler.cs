using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Booking.Queries
{
    public class GetBookingQueryHandler : BaseDbContextWithMediatorHandler<GetBookingQuery, BookingView>
    {
        public GetBookingQueryHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<BookingView> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            BookingView booking = null;
            var bookings = _context.BookingView.Where(o => o.BookingId == request.BookingId)
                .Select(o => new BookingView()
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
                    OpenDay = o.OpenDay,
                    OpenTime = o.OpenTime,
                    CloseTime = o.CloseTime,
                    BookingStartDate = o.BookingStartDate,
                    BookingEndDate = o.BookingEndDate,
                    BookingStatusCode = o.BookingStatusCode,
                    BookingStatusName = o.BookingStatus.SystemVariableName,
                    PaymentMethodCode = o.PaymentMethodCode,
                    PaymentMethodName = o.PaymentMethod.SystemVariableName,
                    Qty = o.Qty,
                    IsOwner = o.IsOwner,
                    CustomerEmail = o.IsOwner ? o.OwnerEmail : o.CustomerEmail,
                    CustomerFirstname = o.IsOwner ? o.OwnerFirstName : o.CustomerFirstname,
                    CustomerLastname = o.IsOwner ? o.OwnerLastName : o.CustomerLastname,
                    CustomerPhoneNumber = o.IsOwner ? o.OwnerPhoneNumber : o.CustomerPhoneNumber,
                    Total = o.Total,
                    Tax = o.Tax,
                    Discount = o.Discount,
                    GrandTotal = o.GrandTotal,
                    Remark = o.Remark,
                    InActiveStatus = o.InActiveStatus,
                    Remaining = o.Remaining
                })
                .ToList();
            if(bookings.Any())
            {
                booking = new BookingView();
                booking = bookings[0];
                bool hasRole = request.IsSuperAdmin;
                bool chk = hasRole;
                #region Check Roles Owner Place
                if(!chk)
                {
                    var places = _context.CompanyProfiles
                        .Where(o => !o.IsDeleted && !o.InActiveStatus 
                            && o.PlaceId == booking.PlaceId && o.Company.OwnerId == request.UserId)
                        .Select(o => o.PlaceId)
                        .ToList();
                    if(places.Any())
                        chk = true;
                }
                #endregion

                #region Check Roles Admin Place
                if(!chk)
                {
                    var places = _context.CompanyProfiles
                        .Where(o => !o.IsDeleted && !o.InActiveStatus 
                            && o.PlaceId == booking.PlaceId && o.AdminId == request.UserId)
                        .Select(o => o.PlaceId)
                        .ToList();
                    if(places.Any())
                        chk = true;
                }
                #endregion

                if(!chk)
                    throw new Exception("Can not Permission");

                #region BookingDate
                List<BookingDateView> bookingDates = new List<BookingDateView>();
                foreach(var b in bookings)
                {
                    bookingDates.Add(
                        new BookingDateView()
                        {
                            BookingDateId = b.BookingDateId,
                            StartDate = b.BookingStartDate,
                            EndDate = b.BookingEndDate,
                            OpenDay = b.OpenDay,
                            OpenTime = b.OpenTime,
                            CloseTime = b.CloseTime
                        }
                    );
                }

                if (bookingDates.Any())
                    bookingDates.OrderByDescending(o => o.StartDate);
                booking.BookingDates = bookingDates;
                #endregion

                #region BookingFacility
                var bookingFacilities = _context.BookingFacility.Where(o => o.BookingId == request.BookingId)
                            .Include(o => o.Facility)
                            .Select(o => new BookingFacilityView()
                            {
                                BookingFacilityId = o.BookingFacilityId,
                                FacilityId = o.FacilityId,
                                FacilityName = o.Facility.FacilityName_TH,
                                Price = o.Price,
                                Qty = o.Qty
                            })
                            .ToList();
                booking.BookingFacilities = bookingFacilities;
                #endregion

                #region Gen QR Code
                QRCodeGeneratorCommand qrCodeCmd = new QRCodeGeneratorCommand();
                List<QRInfo> qrInfos = new List<QRInfo>();
                qrInfos.Add(new QRInfo()
                {
                    Id = request.BookingId.ToString(),
                    Value = booking.BookingNumber,
                    Width = 100,
                    Height = 100
                });
                qrCodeCmd.QRInfos = qrInfos;
                var qrCode = await _mediator.Send(qrCodeCmd);
                if(!qrCode.Status)
                    throw new Exception("ไม่สามารถ Generate QR Code ได้");

                booking.QRCode = qrCode.Data.Result.Any() ? qrCode.Data.Result[0].Base64 : null;
                #endregion

                #region Payment
                var payment = _context.Payment.Where(o => !o.IsDeleted && !o.InActiveStatus && o.BookingId == booking.BookingId)
                    .OrderByDescending(o => o.PaymentDate)                    
                    .FirstOrDefault();
                if(payment != null)
                {
                    booking.LastPaymentDate = payment.PaymentDate;
                    booking.LastPaymentPaid = payment.Paid;
                }
                #endregion
            }
            return await Task.FromResult(booking);
        }
    }
}
