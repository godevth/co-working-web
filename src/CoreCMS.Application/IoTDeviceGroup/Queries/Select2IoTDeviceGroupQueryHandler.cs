using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Booking.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoreCMS.Application.IoTDeviceGroup.Queries
{
    public class Select2IoTDeviceGroupQueryHandler : BaseDbContextWithMediatorHandler<Select2IoTDeviceGroupQuery, CommandResult<OptionViewModel[]>>
    {
        private UserManager<ApplicationUser> _userManager;
        
        public Select2IoTDeviceGroupQueryHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<OptionViewModel[]>> Handle(Select2IoTDeviceGroupQuery request, CancellationToken cancellationToken)
        {
            CommandResult<OptionViewModel[]> cmdResult = new CommandResult<OptionViewModel[]>
            {
                Message = "ไม่สามารถดึงข้อมูลกลุ่มอุปกรณ์ได้"
            };

            try
            {
                #region Check Role Booking
                CheckBookingRoleCommand roleCommand = new CheckBookingRoleCommand()
                {
                    BookingId = request.BookingId,
                    IsSuperAdmin = request.IsSuperAdmin,
                    UserId = request.UserId,
                    TypeUse = "IoTDeviceGroup"
                };
                await _mediator.Send(roleCommand);
                #endregion

                var query = _context.IoTDeviceGroup
                    .AsExpandable();
                OptionViewModel[] result = null;
            
                string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToLower() : string.Empty;

                query = query.Where(o => !o.IsDeleted && !o.InActiveStatus 
                    && (o.GroupName.ToLower().Contains(search) || o.Description.ToLower().Contains(search)));

                if(request.RoomId.HasValue){
                    query = query.Where(o => o.RoomId == request.RoomId.Value);
                }

                var selectQuery = query
                .Select(o => new OptionViewModel
                {
                    Value = o.IoTDeviceGroupId.ToString(),
                    Text = o.GroupName,
                    Data = new {
                        Description = o.Description,
                        RoomId = o.RoomId.ToString(),
                        RoomNameTH = o.Room.RoomName_TH,
                        RoomNameEN = o.Room.RoomName_EN,
                        IsOpenPerDevice = o.IsOpenPerDevice
                    }
                });

                result = await selectQuery.ToArrayAsync();
                cmdResult.Data = result;
                cmdResult.Status = true;
                cmdResult.Message = "ดึงข้อมูลกลุ่มอุปกรณ์สำเร็จ";
            }
            catch (Exception e)
            {
                cmdResult.Message = e.Message;
            }

            return await Task.FromResult<CommandResult<OptionViewModel[]>>(cmdResult);
            
        }
    }
}