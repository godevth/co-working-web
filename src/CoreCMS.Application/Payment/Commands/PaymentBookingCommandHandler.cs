using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Booking.Commands;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.Payment.Commands
{
    public class PaymentBookingCommandHandler : BaseDbContextWithMediatorHandler<PaymentBookingCommand, CommandResult<int>>
    {
        private UserManager<ApplicationUser> _userManager;
        public PaymentBookingCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<int>> Handle(PaymentBookingCommand request, CancellationToken cancellationToken)
        {
            CommandResult<int> cmdResult = new CommandResult<int>()
            {
                Message = "ไม่สามารถชำระเงินได้"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    List<string> reqStatus = new List<string>() { "BOOKING_STATUS_RESERVE", "BOOKING_STATUS_WAITING_FOR_PAYMENT" };
                    var booking = _context.Booking
                        .Include(o => o.BookingFacilities).ThenInclude(bf => bf.Facility)
                        .Where(o => o.BookingId == request.BookingId).SingleOrDefault();
                    if(booking == null)
                        throw new Exception($"ไม่พบข้อมูล Booking Id {request.BookingId}");
                    if (booking.InActiveStatus || booking.IsDeleted 
                        || booking.BookingStatusCode == "BOOKING_STATUS_CANCEL" || booking.BookingStatusCode == "BOOKING_STATUS_PLACE_CANCEL")
                        throw new Exception($"รหัสการจอง {booking.BookingNumber} ถูกยกเลิกไปแล้ว");
                    if (booking.BookingStatusCode == "BOOKING_STATUS_PAID")
                        throw new Exception($"รหัสการจอง {booking.BookingNumber} ชำระค่าบริการไปแล้ว");
                    if (!reqStatus.Contains(booking.BookingStatusCode))
                        throw new Exception($"สถานะการจอง {booking.BookingNumber} ไม่ถูกต้อง");

                    #region Payment
                    Domain.Payment payment = new Domain.Payment()
                    {
                        BookingId = booking.BookingId,
                        Total = booking.Remaining,
                        CounterPaymentCode = request.CounterPaymentCode,
                        Paid = request.Paid,
                        Receive = request.Receive,
                        Change = request.Change,
                        Remaining = request.Remaining,
                        PaymentDate = request.PaymentDateLocal,
                        BankCode = request.BankCode,
                        CreditCardTypeCode = request.CreditCardTypeCode,
                        MerchantId = request.MerchantId,
                        RefCode = request.RefCode,
                        TransactionId = request.TransactionId,
                        ReceiveUserId = request.CreateUserId,
                        CreatedUserId = request.CreateUserId,
                        CreatedDate = DateTime.Now
                    };

                    if(request.IsOnlinePayment)
                    {
                        if(!request.PaymentResponseId.HasValue)
                            throw new Exception($"กรุณาระบุ PaymentResponseId");

                        payment.PaymentResponseId = request.PaymentResponseId;
                        payment.PaymentMethodCode = "PAYMENT_METHOD_ONLINE";
                    }
                    else 
                        payment.PaymentMethodCode = "PAYMENT_METHOD_COD";

                    // Generate Running Number -- Ignore Deleted Status
                    string numberFormat = "R{0:yyMM}-{1:0000}";
                    int running = 1;
                    if (_context.Payment.Select(o => o.ReceiveRunning).Any())
                    {
                        running = _context.Payment.Select(o => o.ReceiveRunning).Max() + 1;
                    }
                    payment.ReceiveRunning = running;
                    payment.ReceiveNumber = string.Format(numberFormat, payment.CreatedDate, running);

                    _context.Payment.Add(payment);
                    _context.SaveChanges();
                    #endregion

                    #region PaymentSlip
                    if(!string.IsNullOrEmpty(request.Picture) && !string.IsNullOrEmpty(request.ContentType))
                    {
                        AddPictureCommand addPictureCmd = new AddPictureCommand()
                        {
                            FileName = Guid.NewGuid().ToString(),
                            CodeRef = payment.PaymentId.ToString(),
                            TypeRef = PictureTypeRef.PaymentSlip,
                            ContentType = request.ContentType,
                            FileBase64 = request.Picture,
                            CreateUserId = request.CreateUserId
                        };
                        var picResult = await _mediator.Send(addPictureCmd);
                        int errorSize = picResult.Errors?.Count ?? 0; //picResult.Errors == null ? 0 : picResult.Errors.Count
                        if (!picResult.Status)
                        {
                            if (errorSize > 0)
                                throw new Exception($"{string.Join(", ", picResult.Errors.Select(o => o.Value.Join(", ")))}");
                            else
                                throw new Exception(picResult.Message);
                        }
                    }
                    #endregion

                    #region Update Booking
                    booking.Remaining = payment.Remaining;
                    if(booking.Remaining <= 0)
                        booking.BookingStatusCode = "BOOKING_STATUS_PAID";
                    _context.Booking.Update(booking);
                    _context.SaveChanges();
                    #endregion

                    #region Send Email
                    var bookingView = _context.HistoryView
                        .Where(o => o.BookingId == request.BookingId).SingleOrDefault();
                    var qty = bookingView.Qty * bookingView.PriceQty;

                    var sendEmailBookingCommand = new SendEmailConfirmPaymentCommand()
                    {
                        HeadSubjectCode = "PAYMENT",
                        PlaceName = bookingView.PlaceNameTH,
                        RoomName = bookingView.RoomNameTH,
                        PlaceAddress = string.Format("{0}<br>หมายเลขโทรศัพท์ {1}", bookingView.PlaceAddress, "-"),
                        BookingQty = string.Format("{0} {1}", qty, bookingView.PriceTimeType),
                        CheckInDate = bookingView.BookingStartDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")),
                        CheckOutDate = bookingView.BookingEndDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")),
                        BookingId = bookingView.BookingId,
                        BookingNo = bookingView.BookingNumber,
                        BookingName = string.Format("คุณ {0} {1}", bookingView.OwnerFirstName, bookingView.OwnerLastName),
                        BookingForName = !booking.IsOwner ? string.Format("คุณ {0} {1}", bookingView.CustomerFirstname, bookingView.CustomerLastname) : "",
                        RoomSeat = string.Format("{0}", bookingView.RoomCapacity),
                        PriceTotal = string.Format("{0:F2}", bookingView.GrandTotal),
                        MapUrl = string.Format("http://maps.google.com/maps?q={0},{1}", bookingView.PlaceLatitude, bookingView.PlaceLongitude),
                        BookingDate = string.Format("{0} {1} น.", bookingView.CreatedDate.Value.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")), bookingView.CreatedDate.Value.ToString("HH:mm")),
                        Remark = bookingView.Remark,
                        BookingEmail = bookingView.OwnerEmail,
                        BookingForEmail = bookingView.CustomerEmail,
                        PictureUrl = _context.Picture.Where(x => x.TypeRef == "Room" && x.CodeRef == bookingView.RoomId.ToString()).Select(x => x.FileInfoId).FirstOrDefault(),
                        PaymentMethodCode = bookingView.PaymentMethodCode,
                        PaymentMethodName = bookingView.PaymentMethodName,
                        Facility = booking.BookingFacilities.Select(x => new EmailBookingFacilities()
                            {
                                FacilityName = x.Facility.FacilityName_TH,
                                FacilityQty = x.Qty,
                                FacilityPrice = string.Format("{0:F2}", x.Price)
                            }).ToList(),
                    };

                    if (bookingView.PriceTimeType.ToLower() == "hours")
                    {
                        sendEmailBookingCommand.CheckInTime = string.Format("(ก่อน {0} น.)", bookingView.BookingStartDate.ToString("HH:mm"));
                        sendEmailBookingCommand.CheckOutTime = string.Format("(หลัง {0} น.)", bookingView.BookingEndDate.ToString("HH:mm"));
                    }
                    
                    if(request.IsOnlinePayment)
                    {
                        var paymentResponse = _context.PaymentResponse.Where(o => o.Id == request.PaymentResponseId).SingleOrDefault();
                        sendEmailBookingCommand.PaymentMethodName += $" [{paymentResponse.ChannelCodeName}]";
                    }

                    await _mediator.Send(sendEmailBookingCommand);
                    #endregion

                    if (!nestedTrans)
                        ts.Commit();

                    cmdResult.Message = "ชำระเงินสำเร็จ";
                    cmdResult.Data = payment.PaymentId;
                    cmdResult.Status = true;
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