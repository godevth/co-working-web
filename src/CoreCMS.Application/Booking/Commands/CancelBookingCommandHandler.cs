using CoreCMS.Application.Notification.Commands;
using CoreCMS.Application.Notification.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Booking.Commands
{
    public class CancelBookingCommandHandler : BaseDbContextWithMediatorHandler<CancelBookingCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        private readonly CowConfig _config;
        public CancelBookingCommandHandler(ApplicationDbContext context, IMediator mediator, IOptions<CowConfig> config) : base(context, mediator)
        {
            _config = config.Value;
            _mediator = mediator;
        }

        public override async Task<CommandResult<bool>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>()
            {
                Message = request.Language == "TH" ? "ไม่สามารถยกเลิก Booking ได้" : "Can't cancel booking."
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    if (string.IsNullOrEmpty(request.UpdateUserId))
                        throw new Exception("ไม่พบข้อมูลผู้ใช้งาน กรุณาล็อกคอิน");

                    var booking = _context.Booking.Where(o => o.BookingId == request.BookingId)
                    .Include(o => o.CreatedUser)
                    .Include(o => o.BookingFacilities)
                    .ThenInclude(o => o.Facility)
                    .SingleOrDefault();

                    if (_context.BookingDate.Where(o => o.BookingId == request.BookingId).Min(o => o.StartDate) <= DateTime.Now.AddMinutes(-_config.CancelBookingTime))
                        throw new Exception(request.Language == "TH" ? $"เกินวันที่จองแล้ว ไม่สามารถยกเลิกจอง" : $"Time out Can't cancel booking.");

                    if (booking == null)
                    {
                        throw new Exception("ไม่พบข้อมูลการจอง");
                    }

                    #region cancel message alert before 15 and 5 minute
                    var listmessagecancel = new MessageCanceledCommand()
                    {
                        AppId = "coworkingspace",
                        RefCode = request.BookingId.ToString(),
                        NotificationType = "BOOKING_MENU"
                    };

                    await _mediator.Send(listmessagecancel);
                    #endregion

                    booking.UpdatedDate = DateTime.Now;
                    booking.UpdatedUserId = request.UpdateUserId;
                    booking.InActiveStatus = true;
                    booking.IsDeleted = true;
                    
                    //เช็คการคืนเงิน
                    if(booking.Remaining < booking.GrandTotal)
                        booking.VoidCode = "VOID_TYPE_IN_PROGRESS";

                    //ยกเลิกโดยผู้จอง
                    if(request.IsMobile)
                        booking.BookingStatusCode = "BOOKING_STATUS_CANCEL";
                    //ยกเลิกโดยสถานที่
                    else 
                        booking.BookingStatusCode = "BOOKING_STATUS_PLACE_CANCEL";

                    _context.Booking.Update(booking);
                    int rows = _context.SaveChanges();
                    if (rows > 0)
                    {
                        var bookingview = _context.BookingView.Where(x => x.BookingId == request.BookingId).FirstOrDefault();
                        var placeId = _context.BookingView.Where(x => x.BookingId == request.BookingId).Select(x=>x.PlaceId).FirstOrDefault();
                        var place = _context.Place.Where(o => !o.IsDeleted && !o.InActiveStatus && o.PlaceId == placeId)
                        .Include(o => o.Company)
                        .ThenInclude(o => o.Owner)
                        .FirstOrDefault();
                        var room = _context.Room.Where(o => !o.IsDeleted && !o.InActiveStatus && o.RoomId == booking.RoomId).FirstOrDefault();

                        if (booking.CreatedUser.DisplayName == null)
                        {
                            throw new Exception($"ไม่พบข้อมูลชื่อสกุลผู้จอง");
                        }

                        if (place.Company == null)
                        {
                            throw new Exception($"ไม่พบข้อมูลเจ้าของสถานที่");
                        }

                        if (place.Company.Owner == null)
                        {
                            throw new Exception($"ไม่พบข้อมูลเจ้าของสถานที่");
                        }
                        #region email
                        var sendEmailBookingCommand = new SendEmailBookingCommand()
                        {
                            HeadSubjectCode = "CANCEL_BOOKING",
                            PlaceName = place.PlaceName_TH,
                            RoomName = room.RoomName_TH,
                            PlaceAddress = string.Format("{0}<br>หมายเลขโทรศัพท์ {1}", place.Address, "-"),
                            BookingQty = string.Format("{0} {1}", bookingview.Qty, bookingview.PriceTimeType),
                            CheckInDate = bookingview.BookingStartDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")),
                            CheckOutDate = bookingview.BookingEndDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")),
                            BookingId = bookingview.BookingId,
                            BookingNo = bookingview.BookingNumber,
                            BookingName = string.Format("คุณ {0}", booking.CreatedUser.DisplayName),
                            BookingForName = !bookingview.IsOwner ? string.Format("คุณ {0} {1}", bookingview.CustomerFirstname, bookingview.CustomerLastname) : "",
                            RoomSeat = string.Format("{0}", bookingview.RoomCapacity),
                            Facility = booking.BookingFacilities.Select(x => new EmailBookingFacilities()
                            {
                                FacilityName = x.Facility.FacilityName_TH,
                                FacilityQty = x.Qty,
                                FacilityPrice = string.Format("{0:F2}", x.Price)
                            }).ToList(),
                            PriceTotal = string.Format("{0:F2}", bookingview.GrandTotal),
                            MapUrl = string.Format("http://maps.google.com/maps?q={0},{1}", bookingview.PlaceLatitude, bookingview.PlaceLongitude),
                            BookingDate = string.Format("{0} {1} น.", bookingview.CreatedDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")), bookingview.CreatedDate.ToString("HH:mm")),
                            Remark = bookingview.Remark,
                            BookingEmail = booking.CreatedUser.Email,
                            BookingForEmail = bookingview.CustomerEmail,
                            CompanyName = string.Format("คุณ {0}", place.Company.Owner.DisplayName),
                            CompanyEmail = place.Company.Owner.Email,
                            PictureUrl = _context.Picture.Where(x => x.TypeRef == "Room" && x.CodeRef == bookingview.RoomId.ToString()).Select(x => x.FileInfoId).FirstOrDefault(),
                            PaymentMethodCode = booking.PaymentMethodCode
                        };
                        if (bookingview.PriceTimeType == "Hours")
                        {
                            sendEmailBookingCommand.CheckInTime = string.Format("(ก่อน {0} น.)", bookingview.BookingStartDate.ToString("HH:mm"));
                            sendEmailBookingCommand.CheckOutTime = string.Format("(หลัง {0} น.)", bookingview.BookingEndDate.ToString("HH:mm"));
                        }

                        await _mediator.Send(sendEmailBookingCommand);
                        #endregion

                        #region Notification for Booking My self
                        MessageCommand message = new MessageCommand()
                        {
                            UserId = booking.CreatedUserId,
                            CreatedUserId = request.UpdateUserId,
                            IsAllDevice = false,
                            NotificationLink = "",
                            NotificationType = "BOOKING_MENU",
                            RefCode = booking.BookingId.ToString(),
                            SendDateTime = DateTime.Now,
                            SendTimeStamp = DateTime.Now,
                        };
                        var listNoti = new List<MessageNotis>();
                        listNoti.Add(new MessageNotis()
                        {
                            Subject = "แจ้งเตือนการยกเลิกการจอง",
                            Detail = string.Format("หมายเลข {0} \n{1} {2} \n{3} \nระบบได้ทําการยกเลิกการจองเรียบร้อยแล้ว", bookingview.BookingNumber, bookingview.PlaceNameTH, bookingview.RoomNameTH, bookingview.BookingStartDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH"))),
                            Language = "TH"
                        });
                        //listNoti.Add(new MessageNotis()
                        //{
                        //    Subject = "Booking Canceled",
                        //    Detail = "Cancel Booking Success",
                        //    Language = "EN"
                        //});
                        message.MessageNoti = listNoti;
                        await _mediator.Send(message);
                        #endregion

                        #region Notification for Booking for someone
                        var userBookingfor = _context.Users.Where(x => x.Email == bookingview.CustomerEmail).Select(x => x.Id).SingleOrDefault();

                        if (!string.IsNullOrEmpty(userBookingfor))
                        {
                            MessageCommand messageFor = new MessageCommand()
                            {
                                UserId = userBookingfor,
                                CreatedUserId = userBookingfor,
                                IsAllDevice = false,
                                NotificationLink = "",
                                NotificationType = "BOOKING_MENU",
                                RefCode = booking.BookingId.ToString(),
                                SendDateTime = DateTime.Now,
                                SendTimeStamp = DateTime.Now,
                            };
                            var listNotiFor = new List<MessageNotis>();
                            listNotiFor.Add(new MessageNotis()
                            {
                                Subject = "แจ้งเตือนการยกเลิกการจอง",
                                Detail = string.Format("หมายเลข {0} \n{1} {2} \n{3} \nระบบได้ทําการยกเลิกการจองเรียบร้อยแล้ว", bookingview.BookingNumber, bookingview.PlaceNameTH, bookingview.RoomNameTH, bookingview.BookingStartDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH"))),
                                Language = "TH"
                            });
                            //listNoti.Add(new MessageNotis()
                            //{
                            //    Subject = "Booking Canceled",
                            //    Detail = "Cancel Booking Success",
                            //    Language = "EN"
                            //});
                            messageFor.MessageNoti = listNotiFor;
                            await _mediator.Send(messageFor);
                        }
                        #endregion

                        _context.SaveChanges();
                        if (!nestedTrans)
                            ts.Commit();

                        cmdResult.Message = request.Language == "TH" ? "ยกเลิก Booking สำเร็จ" : "Cancel Booking Success";
                        cmdResult.Data = true;
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
