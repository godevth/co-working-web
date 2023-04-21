using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.Booking.Commands
{
    public class CheckOutCommandHandler : BaseDbContextWithMediatorHandler<CheckOutCommand, CommandResult<int>>
    {
        private readonly CowConfig _config;
        private UserManager<ApplicationUser> _userManager;
        
        public CheckOutCommandHandler(ApplicationDbContext context, IMediator mediator, IOptions<CowConfig> config, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _config = config.Value;
            _userManager = userManager;
        }

        public override async Task<CommandResult<int>> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            CommandResult<int> cmdResult = new CommandResult<int>()
            {
                Message = "ไม่สามารถเช็คเอาต์ได้"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    var booking = _context.HistoryView.Where(o => o.BookingId == request.BookingId).SingleOrDefault();
                    string[] cancelStatus = new string[]{ "BOOKING_STATUS_CANCEL", "BOOKING_STATUS_PLACE_CANCEL" };
                    if(booking == null)
                        throw new Exception($"ไม่พบข้อมูล Booking Id {request.BookingId}");
                    if (booking.InActiveStatus || booking.IsDeleted || cancelStatus.Contains(booking.BookingStatusCode))
                        throw new Exception($"รหัสการจอง {booking.BookingNumber} ถูกยกเลิกไปแล้ว");
                    if (booking.BookingStatusCode == "BOOKING_STATUS_RESERVE")
                        throw new Exception($"รหัสการจอง {booking.BookingNumber} ยังไม่ชำระค่าบริการ");

                    #region Check Role 
                    IsRoleOwnerAdminPlaceCommand isRoleCmd = new IsRoleOwnerAdminPlaceCommand()
                    {
                        PlaceId = booking.PlaceId,
                        IsSuperAdmin = request.IsSuperAdmin,
                        UserId = request.CreateUserId
                    };
                    bool isAdmin = await _mediator.Send(isRoleCmd);

                    //Owner Place, Admin Place
                    bool hasRole = isAdmin;
                    if(!hasRole)
                    {
                        if(booking.IsOwner)
                        {
                            //is owner
                            if(booking.OwnerId == request.CreateUserId)
                                hasRole = true;
                        }
                        else 
                        {
                            //จองแทน
                            var user = await _userManager.FindByEmailAsync(booking.CustomerEmail);
                            if(user == null)
                                throw new Exception($"ไม่พบข้อมูลผู้ใช้งาน {request.CreateUserId}");
                            if (user.InActiveStatus || user.IsDeleted)
                                throw new Exception($"ผู้ใช้งาน {user.Email} ถูกยกเลิกไปแล้ว");

                            if(booking.CustomerEmail.ToLower() == user.Email.ToLower())
                                hasRole = true;
                        }
                    }
                    
                    if(!hasRole)
                        throw new Exception($"ไม่มีสิทธิ์เช็คเอาต์");
                    #endregion

                    var bookingDate = _context.BookingDate
                            .Where(o => o.BookingId == booking.BookingId 
                                && o.StartDate.Date <= DateTime.Today.Date && o.EndDate.Date >= DateTime.Today.Date)
                            .FirstOrDefault();

                    if(bookingDate == null)
                        throw new Exception($"รหัสการจอง {booking.BookingNumber} ไม่อยู่ในช่วงเวลาที่ให้เช็คเอาต์");

                    #region CheckOut Dup
                    var checkIn = _context.CheckIn
                        .Where(o => !o.IsDeleted && !o.InActiveStatus && o.BookingId == booking.BookingId)
                        .OrderByDescending(o => o.CheckInDate)
                        .FirstOrDefault();
                    if(checkIn == null)
                        throw new Exception($"รหัสการจอง {booking.BookingNumber} ยังไม่ทำการเช็คอิน");
                    else
                    {
                        int comp =  DateTime.Compare(checkIn.CheckInDate.Value.Date, DateTime.Today);
                        if(comp == 0 && checkIn.CheckOutDate.HasValue)
                            throw new Exception($"เช็คเอาต์ซ้ำ ซึ่งรหัสการจอง {booking.BookingNumber} ได้เช็คเอาต์ {checkIn.CheckOutDate.Value.ToString("dd/MM/yyyy HH:mm")}");
                        else if(comp < 0)
                            throw new Exception($"รหัสการจอง {booking.BookingNumber} ยังไม่ทำการเช็คอิน");
                    }
                    #endregion

                    #region CheckOut
                    checkIn.CheckOutUserId = request.CreateUserId;
                    checkIn.CheckOutDate = DateTime.Now;
                    checkIn.UpdatedUserId =  request.CreateUserId;
                    checkIn.UpdatedDate = checkIn.CheckOutDate;
                    _context.CheckIn.Update(checkIn);
                    #endregion

                    #region Update Booking Date
                    bookingDate.IsCheckOut = true;
                    _context.BookingDate.Update(bookingDate);
                    #endregion

                    #region Update Booking Status
                    var isBookingDate = _context.BookingDate
                            .Where(o => o.BookingId == booking.BookingId 
                                && o.StartDate.Date > DateTime.Today.Date)
                            .Any();
                    if(!isBookingDate)
                    {
                        var bookingUpd = _context.Booking.Where(o => o.BookingId == request.BookingId).SingleOrDefault();
                        bookingUpd.BookingStatusCode = "BOOKING_STATUS_COMPLETE";
                        bookingUpd.UpdatedDate = DateTime.Now;
                        bookingUpd.UpdatedUserId = request.CreateUserId;
                        _context.Booking.Update(bookingUpd);
                    }
                    #endregion

                    int rows = _context.SaveChanges();
                    if (rows > 0)
                    {
                        if (!nestedTrans)
                            ts.Commit();
                            
                        cmdResult.Message = "เช็คเอาต์สำเร็จ";
                        cmdResult.Data = checkIn.CheckInId;
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