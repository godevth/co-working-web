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

namespace CoreCMS.Application.IoTDevice.Queries
{
    public class Select2IoTDeviceQueryHandler : BaseDbContextWithMediatorHandler<Select2IoTDeviceQuery, CommandResult<OptionViewModel[]>>
    {
        private UserManager<ApplicationUser> _userManager;
        
        public Select2IoTDeviceQueryHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<OptionViewModel[]>> Handle(Select2IoTDeviceQuery request, CancellationToken cancellationToken)
        {
            CommandResult<OptionViewModel[]> cmdResult = new CommandResult<OptionViewModel[]>
            {
                Message = "ไม่สามารถดึงข้อมูลอุปกรณ์ได้"
            };

            try
            {
                #region Check Role Booking
                CheckBookingRoleCommand roleCommand = new CheckBookingRoleCommand()
                {
                    BookingId = request.BookingId,
                    IsSuperAdmin = request.IsSuperAdmin,
                    UserId = request.UserId,
                    TypeUse = "IoTDevice"
                };
                await _mediator.Send(roleCommand);
                #endregion

                var query = _context.IoTDevice
                    .AsExpandable();
                OptionViewModel[] result = null;
            
                string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToLower() : string.Empty;

                query = query.Where(o => !o.IsDeleted && !o.InActiveStatus 
                    && (o.DeviceName.ToLower().Contains(search) || o.Description.ToLower().Contains(search)));

                if(request.IoTDeviceGroupId.HasValue){
                    query = query.Where(o => o.IoTDeviceGroupId == request.IoTDeviceGroupId.Value);
                }

                var selectQuery = query
                .Select(o => new OptionViewModel
                {
                    Value = o.IoTDeviceId.ToString(),
                    Text = o.DeviceName,
                    Data = new {
                        Description = o.Description,
                        IoTDeviceGroupId = o.IoTDeviceGroupId,
                        StatusCode = o.StatusCode,
                        StatusName = o.DeviceStatus.SystemVariableName,
                        DeviceTypeCode = o.DeviceTypeCode,
                        DeviceTypeName = o.DeviceType.SystemVariableName,
                        Dimmer = o.Dimmer
                    }
                });

                result = await selectQuery.ToArrayAsync();
                cmdResult.Data = result;
                cmdResult.Status = true;
                cmdResult.Message = "ดึงข้อมูลอุปกรณ์สำเร็จ";
            }
            catch (Exception e)
            {
                cmdResult.Message = e.Message;
            }

            return await Task.FromResult<CommandResult<OptionViewModel[]>>(cmdResult);
        }
    }
}