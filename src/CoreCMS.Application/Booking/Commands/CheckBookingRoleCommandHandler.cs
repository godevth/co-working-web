using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CoreCMS.Application.Booking.Commands
{
    public class CheckBookingRoleCommandHandler : BaseDbContextWithMediatorHandler<CheckBookingRoleCommand, HistoryView>
    {
        private UserManager<ApplicationUser> _userManager;
        
        public CheckBookingRoleCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<HistoryView> Handle(CheckBookingRoleCommand request, CancellationToken cancellationToken)
        {
            var booking = _context.HistoryView.Where(o => o.BookingId == request.BookingId).SingleOrDefault();
            if(booking == null)
                throw new Exception($"ไม่พบข้อมูล Booking Id {request.BookingId}");
            if (booking.InActiveStatus || booking.IsDeleted || booking.BookingStatusCode == "BOOKING_STATUS_CANCEL")
                throw new Exception($"รหัสการจอง {booking.BookingNumber} ถูกยกเลิกไปแล้ว");

            #region Check Role 
            IsRoleOwnerAdminPlaceCommand isRoleCmd = new IsRoleOwnerAdminPlaceCommand()
            {
                PlaceId = booking.PlaceId,
                IsSuperAdmin = request.IsSuperAdmin,
                UserId = request.UserId
            };
            bool isAdmin = await _mediator.Send(isRoleCmd);

            //Owner Place, Admin Place
            bool hasRole = isAdmin;
            if(!hasRole)
            {
                if(booking.IsOwner)
                {
                    //is owner
                    if(booking.OwnerId == request.UserId)
                        hasRole = true;
                }
                else 
                {
                    //จองแทน
                    var user = await _userManager.FindByEmailAsync(booking.CustomerEmail);
                    if(user == null)
                        throw new Exception($"ไม่พบข้อมูลผู้ใช้งาน {request.UserId}");
                    if (user.InActiveStatus || user.IsDeleted)
                        throw new Exception($"ผู้ใช้งาน {user.Email} ถูกยกเลิกไปแล้ว");

                    if(booking.CustomerEmail.ToLower() == user.Email.ToLower())
                        hasRole = true;
                }
            }
            
            if(!hasRole)
            {
                if(request.TypeUse == "IoTDeviceGroup")
                    throw new Exception($"ไม่มีสิทธิ์ดึงข้อมูลกลุ่มอุปกรณ์");
                else if(request.TypeUse == "IoTDevice")
                    throw new Exception($"ไม่มีสิทธิ์ดึงข้อมูลอุปกรณ์");
                else if(request.TypeUse == "ChangeStatusDevice")
                    throw new Exception($"ไม่มีสิทธิ์เปลี่ยนสถานะของอุปกรณ์");
                else
                    throw new Exception($"ไม่มีสิทธิ์ใช้งาน");
            }
                
            #endregion

            #region ChecIn/CheckOut
            var bookingDate = _context.BookingDate
                    .Where(o => o.BookingId == booking.BookingId 
                        && o.StartDate.Date == DateTime.Today.Date && o.EndDate.Date == DateTime.Today.Date)
                    .FirstOrDefault();

            if(bookingDate == null)
                throw new Exception($"รหัสการจอง {booking.BookingNumber} ยังไม่ทำการเช็คอิน");

            if(!bookingDate.IsCheckIn)
                throw new Exception($"รหัสการจอง {booking.BookingNumber} ยังไม่ทำการเช็คอิน");
            
            if(bookingDate.IsCheckOut)
                throw new Exception($"รหัสการจอง {booking.BookingNumber} ได้เช็คเอาต์ไปแล้ว");

            if(bookingDate.EndDate < DateTime.Now)
                    throw new Exception($"รหัสการจอง {booking.BookingNumber} หมดเวลาจอง");
            #endregion

            return await Task.FromResult(booking);
        }
    }
}