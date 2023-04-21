using CoreCMS.Application.Notification.Commands;
using CoreCMS.Application.Notification.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Booking.Commands
{
   public class AlertBookingCommandHandler : BaseDbContextWithMediatorHandler<AlertBookingCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        private readonly CowConfig _config;
        public AlertBookingCommandHandler(ApplicationDbContext context, IMediator mediator, IOptions<CowConfig> config) : base(context,mediator)
        {
            _config = config.Value;
            _mediator = mediator;
        }

        public override async Task<CommandResult<bool>> Handle(AlertBookingCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถแจ้งเตือน Booking ล่วงหน้าได้"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    DateTime s = DateTime.Now.AddDays(1);
                    DateTime e = DateTime.Now.AddDays(1);;
                    s = new DateTime(s.Year, s.Month, s.Day, 0, 0, 0);
                    e = new DateTime(e.Year, e.Month, e.Day, 23, 59, 0);
                    // s = new DateTime(s.Year, s.Month, 14, 0, 0, 0);
                    // e = new DateTime(e.Year, e.Month, 14, 23, 59, 0);
                    var getBooking = _context.HistoryView.Where(o=>!o.IsDeleted && !o.InActiveStatus && o.BookingStartDate>= s &&o.BookingStartDate<=e).ToList();
                    if(getBooking!=null && getBooking.Count>0){
                        foreach (var item in getBooking) {
                            var userIds = "";
                            if (item.CustomerEmail!=null) {
                                userIds = _context.Users.Where(o => o.Email == item.CustomerEmail).Select(o=>o.Id).FirstOrDefault();
                                MessageCommand message = new MessageCommand()
                                {
                                    UserId = userIds,
                                    CreatedUserId = userIds,
                                    IsAllDevice = false,
                                    NotificationLink = "",
                                    NotificationType = "BOOKING_MENU",
                                    RefCode = item.BookingId.ToString(),
                                    SendDateTime = DateTime.Now,
                                    SendTimeStamp = DateTime.Now,
                                };
                                var listNoti = new List<MessageNotis>();
                                listNoti.Add(new MessageNotis()
                                {
                                    Subject = "แจ้งเตือน",
                                    Detail = "แจ้งเตือนการจองล่วงหน้า",
                                    Language = "TH"
                                });
                                listNoti.Add(new MessageNotis()
                                {
                                    Subject = "Message",
                                    Detail = "Alert Booking Success",
                                    Language = "EN"
                                });
                                message.MessageNoti = listNoti;
                                var res = await _mediator.Send(message);
                            }
                            MessageCommand messageOwner = new MessageCommand()
                            {
                                UserId = item.OwnerId,
                                CreatedUserId = item.OwnerId,
                                IsAllDevice = false,
                                NotificationLink = "",
                                NotificationType = "BOOKING_MENU",
                                RefCode = item.BookingId.ToString(),
                                SendDateTime = DateTime.Now,
                                SendTimeStamp = DateTime.Now,
                            };
                            var listNotiOwner = new List<MessageNotis>();
                            listNotiOwner.Add(new MessageNotis()
                            {
                                Subject = "แจ้งเตือน",
                                Detail = "แจ้งเตือนการจองล่วงหน้า",
                                Language = "TH"
                            });
                            listNotiOwner.Add(new MessageNotis()
                            {
                                Subject = "Message",
                                Detail = "Alert Booking Success",
                                Language = "EN"
                            });
                            messageOwner.MessageNoti = listNotiOwner;
                            var resOwner = await _mediator.Send(messageOwner);
                        }
                    }
                        _context.SaveChanges();
                        if (!nestedTrans)
                            ts.Commit();

                        cmdResult.Message = "แจ้งเตือน Booking ล่วงหน้าสำเร็จ" ;
                        cmdResult.Data = true;
                        cmdResult.Status = true;
                    //}
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
