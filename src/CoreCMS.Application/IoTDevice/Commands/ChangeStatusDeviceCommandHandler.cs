using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiIoT.Commands;
using CoreCMS.Application.Booking.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CoreCMS.Application.IoTDevice.Commands
{
    public class ChangeStatusDeviceCommandHandler : BaseDbContextWithMediatorHandler<ChangeStatusDeviceCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;
        
        public ChangeStatusDeviceCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<string>> Handle(ChangeStatusDeviceCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถเปลี่ยนสถานะของอุปกรณ์ได้"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    #region Check Role Booking
                    CheckBookingRoleCommand roleCommand = new CheckBookingRoleCommand()
                    {
                        BookingId = request.BookingId,
                        IsSuperAdmin = request.IsSuperAdmin,
                        UserId = request.CreateUserId,
                        TypeUse = "ChangeStatusDevice"
                    };
                    var booking = await _mediator.Send(roleCommand);
                    #endregion

                    #region IoTDevice
                    if(request.Dimmer.HasValue && (request.Dimmer.Value <= 0 || request.Dimmer.Value > 100))
                        throw new Exception($"ระบุ Dimmer ไม่ถูกต้อง");

                    int rows = 0;
                    //Device
                    if(request.IoTDeviceId.HasValue)
                    {
                        var deviceGroup = _context.IoTDeviceGroup.Where(o => o.IoTDeviceGroupId == request.IoTDeviceGroupId).FirstOrDefault();
                        if(deviceGroup == null)
                            throw new Exception($"ไม่พบข้อมูลกลุ่มอุปกรณ์ {request.IoTDeviceGroupId}");
                        if (deviceGroup.InActiveStatus || deviceGroup.IsDeleted)
                            throw new Exception($"กลุ่มอุปกรณ์ {deviceGroup.GroupName} ถูกยกเลิกไปแล้ว");
                        if(booking.RoomId != deviceGroup.RoomId)
                            throw new Exception($"ไม่พบข้อมูลอุปกรณ์ในห้อง {booking.RoomNameTH}");
                        if(!deviceGroup.IsOpenPerDevice)
                            throw new Exception($"ไม่อนุญาตให้เปิดอุปกรณ์ในห้อง {booking.RoomNameTH} แบบแยกอุปกรณ์");

                        var device = _context.IoTDevice.Where(o => o.IoTDeviceId == request.IoTDeviceId && o.IoTDeviceGroup.RoomId == booking.RoomId).FirstOrDefault();
                        if(device == null)
                            throw new Exception($"ไม่พบข้อมูลอุปกรณ์ {request.IoTDeviceId}");
                        if (device.InActiveStatus || device.IsDeleted)
                            throw new Exception($"อุปกรณ์ {device.DeviceName} ถูกยกเลิกไปแล้ว");

                        IoTControlCommand iotCmd = new IoTControlCommand()
                        {
                            MongoDeviceId = device.MongoDeviceId,
                            Dimmer = request.Dimmer,
                        };
                        if(request.StatusCode == "IOT_DEVICE_STATUS_OFF")
                            iotCmd.Config = "OFF";
                        else if(request.StatusCode == "IOT_DEVICE_STATUS_ON")
                            iotCmd.Config = "ON";

                        var iotStatus = await _mediator.Send(iotCmd);
                        if(iotStatus)
                        {
                            device.StatusCode = request.StatusCode;
                            device.UpdatedDate = DateTime.Now;
                            device.UpdatedUserId = request.CreateUserId;
                            _context.IoTDevice.Update(device);

                            IoTTransaction ioTTransaction = new IoTTransaction()
                            {
                                IoTDeviceGroupId = request.IoTDeviceGroupId,
                                IoTDeviceId = request.IoTDeviceId,
                                Dimmer = request.Dimmer,
                                StatusCode = request.StatusCode,
                                CreatedDate = DateTime.Now,
                                CreatedUserId = request.CreateUserId
                            };
                            _context.IoTTransaction.Add(ioTTransaction);

                            rows = _context.SaveChanges();
                        }
                    }
                    //DeviceGroup
                    else
                    {
                        var deviceGroup = _context.IoTDeviceGroup.Where(o => o.IoTDeviceGroupId == request.IoTDeviceGroupId).FirstOrDefault();
                        if(deviceGroup == null)
                            throw new Exception($"ไม่พบข้อมูลกลุ่มอุปกรณ์ {request.IoTDeviceGroupId}");
                        if (deviceGroup.InActiveStatus || deviceGroup.IsDeleted)
                            throw new Exception($"กลุ่มอุปกรณ์ {deviceGroup.GroupName} ถูกยกเลิกไปแล้ว");
                        if(booking.RoomId != deviceGroup.RoomId)
                            throw new Exception($"ไม่พบข้อมูลอุปกรณ์ในห้อง {booking.RoomNameTH}");

                        var devices = _context.IoTDevice.Where(o => o.IoTDeviceGroupId == request.IoTDeviceGroupId).ToList();
                        foreach(var d in devices)
                        {
                            IoTControlCommand iotCmd = new IoTControlCommand()
                            {
                                MongoDeviceId = d.MongoDeviceId,
                                Dimmer = request.Dimmer,
                            };
                            if(request.StatusCode == "IOT_DEVICE_STATUS_OFF")
                                iotCmd.Config = "OFF";
                            else if(request.StatusCode == "IOT_DEVICE_STATUS_ON")
                                iotCmd.Config = "ON";

                            var iotStatus = await _mediator.Send(iotCmd);
                            if(iotStatus)
                            {
                                d.StatusCode = request.StatusCode;
                                d.UpdatedDate = DateTime.Now;
                                d.UpdatedUserId = request.CreateUserId;
                                _context.IoTDevice.Update(d);

                                IoTTransaction ioTTransaction = new IoTTransaction()
                                {
                                    IoTDeviceGroupId = request.IoTDeviceGroupId,
                                    IoTDeviceId = request.IoTDeviceId,
                                    Dimmer = request.Dimmer,
                                    StatusCode = request.StatusCode,
                                    CreatedDate = DateTime.Now,
                                    CreatedUserId = request.CreateUserId
                                };
                                _context.IoTTransaction.Add(ioTTransaction);

                                rows += _context.SaveChanges();
                            }
                        }
                    }
                   
                    #endregion
                    
                    if (rows > 0)
                    {
                        if (!nestedTrans)
                            ts.Commit();
                            
                        cmdResult.Message = "เปลี่ยนสถานะของอุปกรณ์สำเร็จ";
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