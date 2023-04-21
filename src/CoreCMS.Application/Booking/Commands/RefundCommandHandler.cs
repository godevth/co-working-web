using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoreCMS.Application.Booking.Commands
{
    public class RefundCommandHandler : BaseDbContextWithMediatorHandler<RefundCommand, CommandResult<bool>>
    {
        private UserManager<ApplicationUser> _userManager;
        public RefundCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<bool>> Handle(RefundCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถคืนเงินได้"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    var booking = _context.Booking
                        .Include(o => o.Room).ThenInclude(o => o.Place)
                        .Where(o => o.BookingId == request.BookingId)
                        .SingleOrDefault();
                    string[] cancelStatus = new string[]{ "BOOKING_STATUS_CANCEL", "BOOKING_STATUS_PLACE_CANCEL" };
                    if(booking == null)
                        throw new Exception($"ไม่พบข้อมูล Booking Id {request.BookingId}");
                    if (!cancelStatus.Contains(booking.BookingStatusCode))
                        throw new Exception($"รหัสการจอง {booking.BookingNumber} สถานะไม่ถูกต้อง");

                    #region Update Payment
                    var payments = _context.Payment.Where(o => !o.IsDeleted && !o.InActiveStatus && o.BookingId == booking.BookingId).ToList();
                    if(!payments.Any())
                        throw new Exception($"รหัสการจอง {booking.BookingNumber} ยังไม่ชำระค่าบริการ");

                    DateTime dtNow = DateTime.Now;
                    foreach(var p in payments)
                    {
                        p.IsDeleted = true;
                        p.InActiveStatus = true;
                        p.DeletedDate = dtNow;
                        p.DeletedUserId = request.CurrentUser;
                        _context.Payment.Update(p);
                    }
                    #endregion

                    #region Check Roles Owner Place
                    bool chk = request.IsSuperAdmin;
                    if(!chk)
                    {
                        var places = _context.CompanyProfiles
                            .Where(o => !o.IsDeleted && !o.InActiveStatus 
                                && o.PlaceId == booking.Room.PlaceId && o.Company.OwnerId == request.CurrentUser)
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
                                && o.PlaceId == booking.Room.PlaceId && o.AdminId == request.CurrentUser)
                            .Select(o => o.PlaceId)
                            .ToList();
                        if(places.Any())
                            chk = true;
                    }

                    if(!chk)
                        throw new Exception("ไม่มีสิทธิ์คืนเงิน");
                    #endregion

                    booking.VoidCode = "VOID_TYPE_REFUNDED";
                    booking.RefundedBy = request.CurrentUser;
                    booking.RefundedDate = dtNow;
                    _context.Booking.Update(booking);

                    int rows = _context.SaveChanges();
                    if (rows > 0)
                    {
                        if (!nestedTrans)
                            ts.Commit();
                            
                        cmdResult.Message = "คืนเงินสำเร็จ";
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