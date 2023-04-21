using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ImplementationDate.Queries;
using CoreCMS.Application.Notification.Commands;
using CoreCMS.Application.Notification.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoreCMS.Application.Booking.Commands
{
    public class BookingCommandHandler : BaseDbContextWithMediatorHandler<BookingCommand, CommandResult<string>>
    {
        public BookingCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<string>> Handle(BookingCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = new CommandResult<string>()
            {
                Message = request.Language == "EN" ? "Cannot Booking." : "ไม่สามารถ Booking ได้"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    if (string.IsNullOrEmpty(request.CurrentUserId))
                    {
                        var mes = request.Language == "EN" ? "Please log in." : "ไม่ข้อมูลผู้ใช้งาน กรุณาล็อกคอิน";
                        throw new Exception(mes);
                    }

                    #region Calculate Pricing (ตรวจสอบสิทธิ์การจอง, สถานที่เปิดมั๊ย/ห้องว่างมั๊ย)
                    CalculatePricingCommand pricingCommand = new CalculatePricingCommand()
                    {
                        CurrentUserId = request.CurrentUserId,
                        StartDateTime = request.BookingStartDate,
                        EndDateTime = request.BookingEndDate,
                        RoomId = request.RoomId,
                        TimeType = request.TimeType,
                        Discount = request.Discount,
                        Facilities = request.Facilities,
                        Language = request.Language
                    };
                    var price = await _mediator.Send(pricingCommand);
                    if(!price.Status || price.Data == null)
                    {
                        throw new Exception(price.Message);
                    }
                    #endregion

                    var room = _context.Room.Where(o => !o.IsDeleted && !o.InActiveStatus && o.RoomId == request.RoomId)
                        .FirstOrDefault();

                    #region Place
                    var place = _context.Place.Where(o => !o.IsDeleted && !o.InActiveStatus && o.PlaceId == room.PlaceId)
                        .Include(o => o.Company)
                        .ThenInclude(o => o.Owner)
                        .FirstOrDefault();
                    if (place == null)
                    {
                        var mes = request.Language == "EN" ? "Place not found." : $"ไม่พบข้อมูลสถานที่ รหัส {room.PlaceId}";
                        throw new Exception(mes);
                    }

                    if(place.Company == null)
                    {
                        var mes = request.Language == "EN" ? "Place not found." : $"ไม่พบข้อมูลเจ้าของสถานที่";
                        throw new Exception(mes);
                    }
                    if (place.Company.Owner == null)
                    {
                        var mes = request.Language == "EN" ? "Place not found." : $"ไม่พบข้อมูลเจ้าของสถานที่";
                        throw new Exception(mes);
                    }
                    #endregion

                    Domain.Booking booking = new Domain.Booking()
                    {
                        RoomId = request.RoomId,
                        IsOwner = request.IsOwner,
                        BookingStatusCode = "BOOKING_STATUS_RESERVE",
                        PaymentMethodCode = request.PaymentMethodCode,
                        Remark = request.Remark,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = request.CurrentUserId,
                        //Price
                        Qty = price.Data.Qty,
                        RoomPriceId = price.Data.RoomPriceId,
                        Discount = request.Discount,
                        Total = price.Data.Total,
                        Tax = price.Data.Tax,
                        GrandTotal = price.Data.GrandTotal,
                        Remaining = price.Data.GrandTotal,
                    };

                    if (!booking.IsOwner)
                    {
                        if (string.IsNullOrEmpty(request.CustomerEmail))
                        {   
                            var mes = request.Language == "EN" ? "Please assign customer email." : "กรุณาระบุ Customer Email";
                            throw new Exception(mes);
                        }

                        booking.CustomerEmail = request.CustomerEmail;
                        booking.CustomerFirstname = request.CustomerFirstname;
                        booking.CustomerLastname = request.CustomerLastname;
                        booking.CustomerPhoneNumber = request.CustomerPhoneNumber;
                    }

                    #region Get Room Price
                    var roomPrice = _context.RoomPrice.Where(o => !o.IsDeleted && o.Id == booking.RoomPriceId).FirstOrDefault();
                    booking.PriceTimeType = roomPrice.TimeType;
                    booking.PriceQty = roomPrice.Qty;
                    booking.Price = roomPrice.Price;
                    #endregion

                    #region BookingFacilities
                    List<Domain.BookingFacility> bookingFacilities = new List<Domain.BookingFacility>();
                    if (request.Facilities != null && request.Facilities.Any())
                    {
                        var facilityServices = _context.FacilityServices.Where(o => !o.IsDeleted && !o.InActiveStatus && (o.PlaceId == room.PlaceId || o.RoomId == request.RoomId)).ToList();
                        if(!facilityServices.Any())
                        {
                            string ex = request.Language == "EN" ? $"Facilities Not Found" : $"ไม่พบสิ่งอำนวยความสะดวก";
                            throw new Exception(ex);
                        }

                        foreach(var f in request.Facilities)
                        {
                            var fs = facilityServices.Where(o => o.FacilityServicesId == f.FacilityServicesId).FirstOrDefault();
                            if(fs == null)
                            {
                                string ex = request.Language == "EN" ? $"Facilities Not Found" : $"ไม่พบสิ่งอำนวยความสะดวก";
                                throw new Exception(ex);
                            }

                            bookingFacilities.Add(new Domain.BookingFacility()
                            {
                                FacilityId = fs.FacilityId,
                                Price = fs.Price,
                                Qty = f.Qty
                            });
                        }
                    }
                    booking.BookingFacilities = bookingFacilities;
                    #endregion

                    #region BookingDates
                    SearchImplementationDateQuery implDateQuery = new SearchImplementationDateQuery()
                    {
                        PlaceId = room.PlaceId
                    };
                    var implDates = await _mediator.Send(implDateQuery);
                    if (!implDates.Any())
                    {
                        var mes = request.Language == "EN" ? $"{place.PlaceName_EN} Close service." : $"{place.PlaceName_TH} ไม่อยู่ในช่วงให้บริการ";
                        throw new Exception(mes);
                    }
                    
                    List<Domain.BookingDate> bookingDates = new List<Domain.BookingDate>();
                    DateTime sdt = request.BookingStartDateLocal;
                    DateTime edt = request.BookingEndDateLocal;
                    while (sdt <= request.BookingEndDateLocal)
                    {
                        var implDate = implDates.Where(o => o.StartDay == sdt.DayOfWeek.ToString()).FirstOrDefault();
                        if (implDate == null && request.TimeType.ToLower() == "monthly")
                        {
                            sdt = sdt.AddDays(1);
                            continue;
                        }
                        else if (implDate == null)
                        {
                            var mes = request.Language == "EN" ? $"{place.PlaceName_EN} Close service." : $"{place.PlaceName_TH} ไม่อยู่ในช่วงให้บริการ";
                            throw new Exception(mes);
                        }

                        if (request.TimeType.ToLower() == "daily" || request.TimeType.ToLower() == "monthly")
                        {
                            DateTime startDt = new DateTime(sdt.Year, sdt.Month, sdt.Day, int.Parse(implDate.StartTimeSplit[0]), int.Parse(implDate.StartTimeSplit[1]), 0);
                            DateTime endDt = new DateTime(sdt.Year, sdt.Month, sdt.Day, int.Parse(implDate.EndTimeSplit[0]), int.Parse(implDate.EndTimeSplit[1]), 0);

                            bookingDates.Add(new Domain.BookingDate()
                            {
                                StartDate = startDt,
                                EndDate = endDt,
                                OpenDay = implDate.StartDay,
                                OpenTime = implDate.StartTime,
                                CloseTime = implDate.EndTime
                            });
                        }
                        else
                        {
                            DateTime endDt = new DateTime(sdt.Year, sdt.Month, sdt.Day, edt.Hour, edt.Minute, edt.Second);
                            bookingDates.Add(new Domain.BookingDate()
                            {
                                StartDate = sdt,
                                EndDate = endDt,
                                OpenDay = implDate.StartDay,
                                OpenTime = implDate.StartTime,
                                CloseTime = implDate.EndTime
                            });
                        }

                        sdt = sdt.AddDays(1);
                    }
                    booking.BookingDates = bookingDates;
                    if (!bookingDates.Any())
                    {
                        var mes = request.Language == "EN" ? $"{place.PlaceName_EN} Close service." : $"{place.PlaceName_TH} ไม่อยู่ในช่วงให้บริการ";
                        throw new Exception(mes);
                    }
                    #endregion

                    // Generate Running Number -- Ignore Deleted Status
                    string numberFormat = "{0:yyMM}-{1:0000}";
                    int running = 1;
                    if (_context.Booking.Select(o => o.BookingRunning).Any())
                    {
                        running = _context.Booking.Select(o => o.BookingRunning).Max() + 1;
                    }
                    booking.BookingRunning = running;
                    booking.BookingNumber = string.Format(numberFormat, booking.CreatedDate, running);

                    _context.Booking.Add(booking);
                    int rows = _context.SaveChanges();
                    if (rows > 0)
                    {
                        #region email
                        var qty = booking.Qty * booking.PriceQty;
                        var userCreate = _context.Users.Where(x => x.Id == request.CurrentUserId).FirstOrDefault();

                        if (userCreate.DisplayName == null)
                        {
                            var mes = request.Language == "EN" ? $"Lastname of the reservation was not found." : $"ไม่พบข้อมูลชื่อสกุลผู้จอง";
                            throw new Exception(mes);
                        }

                        var sendEmailBookingCommand = new SendEmailBookingCommand()
                        {
                            HeadSubjectCode = "BOOKING",
                            PlaceName = place.PlaceName_TH,
                            RoomName = room.RoomName_TH,
                            PlaceAddress = string.Format("{0}<br>หมายเลขโทรศัพท์ {1}", place.Address, "-"),
                            BookingQty = string.Format("{0} {1}", qty, request.TimeType),
                            CheckInDate = request.BookingStartDateLocal.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")),
                            CheckOutDate = request.BookingEndDateLocal.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")),
                            BookingId = booking.BookingId,
                            BookingNo = booking.BookingNumber,
                            BookingName = string.Format("คุณ {0}", userCreate.DisplayName),
                            BookingForName = !request.IsOwner ? string.Format("คุณ {0} {1}", request.CustomerFirstname, request.CustomerLastname) : "",
                            RoomSeat = string.Format("{0}", room.Capacity),
                            Facility = bookingFacilities.Select(x => new EmailBookingFacilities()
                            {
                                FacilityName = _context.Facility.Where(f => f.FacilityId == x.FacilityId).Select(x => x.FacilityName_TH).FirstOrDefault(),
                                FacilityQty = x.Qty,
                                FacilityPrice = string.Format("{0:F2}", x.Price)
                            }).ToList(),
                            PriceTotal = string.Format("{0:F2}", booking.GrandTotal),
                            MapUrl = string.Format("http://maps.google.com/maps?q={0},{1}", place.Latitude, place.Longitude),
                            BookingDate = string.Format("{0} {1} น.", booking.CreatedDate.Value.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")), booking.CreatedDate.Value.ToString("HH:mm")),
                            Remark = request.Remark,
                            BookingEmail = userCreate.Email,
                            BookingForEmail = request.CustomerEmail,
                            CompanyName = string.Format("คุณ {0}", place.Company.Owner.DisplayName),
                            CompanyEmail = place.Company.Owner.Email,
                            PictureUrl = _context.Picture.Where(x => x.TypeRef == "Room" && x.CodeRef == room.RoomId.ToString()).Select(x => x.FileInfoId).FirstOrDefault(),
                            PaymentMethodCode = request.PaymentMethodCode
                        };
                        if (request.TimeType.ToLower() == "hourly")
                        {
                            sendEmailBookingCommand.CheckInTime = string.Format("(ก่อน {0} น.)", request.BookingStartDateLocal.ToString("HH:mm"));
                            sendEmailBookingCommand.CheckOutTime = string.Format("(หลัง {0} น.)", request.BookingEndDateLocal.ToString("HH:mm"));
                        }

                        await _mediator.Send(sendEmailBookingCommand);
                        #endregion

                        #region format date booking display
                        var dateBookingDisplay = "";
                        if(request.TimeType.ToLower() == "monthly")
                        {
                            dateBookingDisplay = request.BookingStartDateLocal.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH"));
                            var start = booking.BookingDates.OrderBy(o => o.StartDate).ElementAt(0).StartDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("th-TH"));
                            var end = booking.BookingDates.OrderBy(o => o.StartDate).Last().StartDate.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH"));
                            dateBookingDisplay = string.Format("{0} - {1}", start, end);
                        }
                        else if(request.TimeType.ToLower() == "daily")
                        {
                            var start = booking.BookingDates.OrderBy(o=>o.StartDate).ElementAt(0).StartDate.ToString("dd MMMM", CultureInfo.CreateSpecificCulture("th-TH"));
                            var end = booking.BookingDates.OrderBy(o => o.StartDate).Last().StartDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH"));
                            dateBookingDisplay = string.Format("{0} - {1}",start,end);
                        }
                        else
                        {
                            dateBookingDisplay = booking.BookingDates.OrderBy(o => o.StartDate).ElementAt(0).StartDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH"));
                        }
                        #endregion

                        #region Noti for Booking My self
                        var listNoti = new List<MessageNotis>();
                        var listNotiBefore15 = new List<MessageNotis>();
                        var listNotiBefore5 = new List<MessageNotis>();

                        MessageCommand message = new MessageCommand()
                        {
                            UserId = request.CurrentUserId,
                            CreatedUserId = request.CurrentUserId,
                            IsAllDevice = false,
                            NotificationLink = "",
                            NotificationType = "BOOKING_MENU",
                            RefCode = booking.BookingId.ToString(),
                            SendDateTime = DateTime.Now,
                            SendTimeStamp = DateTime.Now,
                        };
                        #region add Noti Booking
                        listNoti.Add(new MessageNotis()
                        {
                            Subject = "การจองของท่านได้รับการยืนยันเรียบร้อยแล้ว",
                            Detail = string.Format("หมายเลข {0} \n{1} {2} \n{3} \nจํานวนผู้ใช้งาน {4} ท่าน", booking.BookingNumber, place.PlaceName_TH, room.RoomName_TH, dateBookingDisplay, room.Capacity),
                            Language = "TH"
                        });
                        listNoti.Add(new MessageNotis()
                        {
                            Subject = "Booking",
                            Detail = "Save Booking Success",
                            Language = "EN"
                        });
                        message.MessageNoti = listNoti;
                        await _mediator.Send(message);
                        #endregion

                        foreach(var book in booking.BookingDates)
                        {
                            #region add Noti alert bofore 15 min
                            listNotiBefore15.Add(new MessageNotis()
                            {
                                Subject = "แจ้งเตือนใกล้ถึงเวลาจองที่ท่านจองแล้ว",
                                Detail = string.Format("หมายเลข {0} \n{1} {2} \nอีก 15 นาทีจะถึงเวลาที่ท่านได้ทําการจองแล้ว", booking.BookingNumber, place.PlaceName_TH, room.RoomName_TH),
                                Language = "TH"
                            });
                            message.SendDateTime = book.StartDate.AddMinutes(-15);
                            message.SendTimeStamp = book.StartDate.AddMinutes(-15);
                            message.MessageNoti = listNotiBefore15;
                            await _mediator.Send(message);
                            #endregion

                            #region add Noti alert bofore 5 min
                            listNotiBefore5.Add(new MessageNotis()
                            {
                                Subject = "กรุณายืนยันการเข้าใช้งานระบบ(Check-in)",
                                Detail = string.Format("หมายเลข {0} \n{1} {2} \nท่านเหลือเวลา อีก 10 นาทีจะหมดเวลายืนยันการจองแล้ว \nหากไม่ยืนยันในเวลาที่กําหนด ระบบจะทําการยกเลิกการจองโดยอัตโนมัติ", booking.BookingNumber, place.PlaceName_TH, room.RoomName_TH),
                                Language = "TH"
                            });
                            message.SendDateTime = book.StartDate.AddMinutes(-5);
                            message.SendTimeStamp = book.StartDate.AddMinutes(-5);
                            message.MessageNoti = listNotiBefore5;
                            await _mediator.Send(message);
                            #endregion
                        }
                        #endregion

                        #region Noti for Booking for someone
                        var userBookingfor = _context.Users.Where(x => x.Email == request.CustomerEmail).Select(x => x.Id).SingleOrDefault();
                        if (!string.IsNullOrEmpty(userBookingfor))
                        {
                            var listNotiFor = new List<MessageNotis>();
                            var listNotiForBefore15 = new List<MessageNotis>();
                            var listNotiForBefore5 = new List<MessageNotis>();

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
                            #region add Noti Booking
                            listNotiFor.Add(new MessageNotis()
                            {
                                Subject = "การจองของท่านได้รับการยืนยันเรียบร้อยแล้ว",
                                Detail = string.Format("หมายเลข {0} \n{1} {2} \n{3} \nจํานวนผู้ใช้งาน {4} ท่าน", booking.BookingNumber, place.PlaceName_TH, room.RoomName_TH, dateBookingDisplay, room.Capacity),
                                Language = "TH"
                            });
                            listNotiFor.Add(new MessageNotis()
                            {
                                Subject = "Booking",
                                Detail = "Save Booking Success",
                                Language = "EN"
                            });
                            messageFor.MessageNoti = listNotiFor;
                            await _mediator.Send(messageFor);
                            #endregion

                            foreach(var book in booking.BookingDates)
                            {
                                #region add Noti alert bofore 15 min
                                listNotiForBefore15.Add(new MessageNotis()
                                {
                                    Subject = "แจ้งเตือนใกล้ถึงเวลาจองที่ท่านจองแล้ว",
                                    Detail = string.Format("หมายเลข {0} \n{1} {2} \nอีก 15 นาทีจะถึงเวลาที่ท่านได้ทําการจองแล้ว", booking.BookingNumber, place.PlaceName_TH, room.RoomName_TH),
                                    Language = "TH"
                                });
                                messageFor.SendDateTime = book.StartDate.AddMinutes(-15);
                                messageFor.SendTimeStamp = book.StartDate.AddMinutes(-15);
                                messageFor.MessageNoti = listNotiForBefore15;
                                await _mediator.Send(messageFor);
                                #endregion

                                #region add Noti alert bofore 5 min
                                listNotiForBefore5.Add(new MessageNotis()
                                {
                                    Subject = "กรุณายืนยันการเข้าใช้งานระบบ(Check-in)",
                                    Detail = string.Format("หมายเลข {0} \n{1} {2} \nท่านเหลือเวลา อีก 10 นาทีจะหมดเวลายืนยันการจองแล้ว \nหากไม่ยืนยันในเวลาที่กําหนด ระบบจะทําการยกเลิกการจองโดยอัตโนมัติ", booking.BookingNumber, place.PlaceName_TH, room.RoomName_TH),
                                    Language = "TH"
                                });
                                messageFor.SendDateTime = book.StartDate.AddMinutes(-5);
                                messageFor.SendTimeStamp = book.StartDate.AddMinutes(-5);
                                messageFor.MessageNoti = listNotiForBefore5;
                                await _mediator.Send(messageFor);
                                #endregion
                            }
                            #endregion
                        }

                        if (!nestedTrans)
                            ts.Commit();

                        cmdResult.Message = request.Language == "EN"? "Booking Success":"บันทึกข้อมูล Booking สำเร็จ";
                        cmdResult.Data = booking.BookingNumber;
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