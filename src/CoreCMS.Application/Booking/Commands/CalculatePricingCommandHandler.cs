using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Privilege.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoreCMS.Application.Booking.Commands
{
    public class CalculatePricingCommandHandler : BaseDbContextWithMediatorHandler<CalculatePricingCommand, CommandResult<CalculatePricing>>
    {
        public CalculatePricingCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<CalculatePricing>> Handle(CalculatePricingCommand request, CancellationToken cancellationToken)
        {
            CommandResult<CalculatePricing> cmdResult = new CommandResult<CalculatePricing>()
            {
                Message = request.Language == "EN" ? "Cannot Calculate Pricing" : "ไม่สามารถคำนวณราคาได้"
            };
            CalculatePricing pricing = new CalculatePricing();
            try
            {
                var room = _context.Room.Where(o => !o.IsDeleted && !o.InActiveStatus && o.RoomId == request.RoomId)
                    .Include(o => o.Place)
                    .FirstOrDefault();
                if (room == null)
                {
                    string ex = request.Language == "EN" ? "Room not found." : $"ไม่พบข้อมูลห้อง รหัส {request.RoomId}";
                    throw new Exception(ex);
                }

                #region ตรวจสอบสิทธิ์การจอง (privilege)
                //แค่สถานะ Private, Private Only
                if(room.Place.SeeingTypeCode == "SEEING_TYPE_PRIVATE" || room.Place.SeeingTypeCode == "SEEING_TYPE_PRIVATE_ONLY")
                {
                    ValidatePrivilegeCommand privilegeCommand = new ValidatePrivilegeCommand()
                    {
                        PlaceId = room.PlaceId,
                        CurrentUserId = request.CurrentUserId
                    };
                    var chk = await _mediator.Send(privilegeCommand);
                    if(!chk)
                    {
                        string ex = request.Language == "EN" ? $"Don't have Permission in {room.RoomName_EN}" : $"ไม่มีสิทธิ์จองห้อง {room.RoomName_TH}";
                        throw new Exception(ex);
                    }
                }
                #endregion

                #region Validate DateTime 
                if(request.TimeType.ToLower() == "monthly")
                {
                    int lastDate = DateTime.DaysInMonth(request.EndDateTimeLocal.Year, request.EndDateTimeLocal.Month);
                    if(request.StartDateTimeLocal.Day != 1)
                    {
                        string ex = request.Language == "EN" ? "Incorrect Time" : "ช่วงเวลาไม่ถูกต้อง";
                        throw new Exception(ex);
                    }
                    else if(request.EndDateTimeLocal.Day != lastDate)
                    {
                        string ex = request.Language == "EN" ? "Incorrect Time" : "ช่วงเวลาไม่ถูกต้อง";
                        throw new Exception(ex);
                    }
                }
                #endregion

                #region สถานที่เปิดมั๊ย/ห้องว่างมั๊ย
                CheckPlaceStatusCommand placeStatusCommand = new CheckPlaceStatusCommand()
                {
                    RoomId = request.RoomId,
                    TimeType = request.TimeType,
                    StartDateTime = request.StartDateTime,
                    EndDateTime = request.EndDateTime,
                    BookingId = request.BookingId
                };
                var status = await _mediator.Send(placeStatusCommand);

                if(!status.IsOpen)
                {
                    string ex = request.Language == "EN" ? $"{room.Place.PlaceName_EN} Close service." : $"{room.Place.PlaceName_TH} ไม่อยู่ในช่วงให้บริการ";
                    throw new Exception(ex);
                }
                else 
                {
                    if(!status.IsAvailable)
                    {
                        string ex = request.Language == "EN" ? $"Room {room.RoomName_EN} Busy." : $"ห้อง {room.RoomName_TH} ไม่ว่าง";
                        throw new Exception(ex);
                    }
                }
                #endregion

                if(request.TimeType.ToLower() == "hourly")
                {
                    var roomPrice = _context.RoomPrice.Where(o => !o.IsDeleted && !o.InActiveStatus && o.RoomId == request.RoomId
                        && o.TimeType == "Hours" && o.Qty == 1).FirstOrDefault();
                    if(roomPrice == null)
                    {
                        string ex = request.Language == "EN" ? $"Price Not Found" : $"ไม่พบราคาของห้อง {room.Place.PlaceName_TH}";
                        throw new Exception(ex);
                    }

                    double hours = DateTimeOffset.FromUnixTimeMilliseconds(request.EndDateTimeUnix).Subtract(DateTimeOffset.FromUnixTimeMilliseconds(request.StartDateTimeUnix)).TotalHours;
                    pricing.PricePerUnit = roomPrice.Price;
                    //pricing.Qty =  Convert.ToInt32(Math.Ceiling(hours));
                    pricing.Qty =  Convert.ToDecimal(hours);
                    pricing.RoomPriceId = roomPrice.Id;
                }
                else if(request.TimeType.ToLower() == "daily")
                {
                    var roomPrice = _context.RoomPrice.Where(o => !o.IsDeleted && !o.InActiveStatus && o.RoomId == request.RoomId
                        && o.TimeType == "Day" && o.Qty == 1).FirstOrDefault();
                    if(roomPrice == null)
                    {
                        string ex = request.Language == "EN" ? $"Price Not Found" : $"ไม่พบราคาของห้อง {room.Place.PlaceName_TH}";
                        throw new Exception(ex);
                    }

                    double days = DateTimeOffset.FromUnixTimeMilliseconds(request.EndDateTimeUnix).Subtract(DateTimeOffset.FromUnixTimeMilliseconds(request.StartDateTimeUnix)).TotalDays;
                    pricing.PricePerUnit = roomPrice.Price;
                    pricing.Qty =  Convert.ToInt32(Math.Ceiling(days)) + 1;
                    pricing.RoomPriceId = roomPrice.Id;
                }
                else if(request.TimeType.ToLower() == "monthly")
                {
                    var roomPrice = _context.RoomPrice.Where(o => !o.IsDeleted && !o.InActiveStatus && o.RoomId == request.RoomId
                        && o.TimeType == "Month" && o.Qty == 1).FirstOrDefault();
                    if(roomPrice == null)
                    {
                        string ex = request.Language == "EN" ? $"Price Not Found" : $"ไม่พบราคาของห้อง {room.Place.PlaceName_TH}";
                        throw new Exception(ex);
                    }

                    pricing.PricePerUnit = roomPrice.Price;
                    pricing.Qty =  (((request.EndDateTimeLocal.Year - request.StartDateTimeLocal.Year) * 12) + request.EndDateTimeLocal.Month - request.StartDateTimeLocal.Month) + 1;
                    pricing.RoomPriceId = roomPrice.Id;
                }

                #region Cal Facilities
                decimal facilityPrice = 0;
                if(request.Facilities.Any())
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

                        facilityPrice += f.Qty * fs.Price;
                    }
                }   
                #endregion

                
                decimal rPrice = pricing.Qty * pricing.PricePerUnit;
                if(pricing.Qty - Math.Truncate(pricing.Qty) > 0)
                    pricing.RoomPrice = Math.Ceiling(rPrice);
                else
                    pricing.RoomPrice = rPrice;

                pricing.FacilityPrice = facilityPrice;
                pricing.Discount = request.Discount;
                pricing.Total = (pricing.RoomPrice + facilityPrice) - request.Discount;
                pricing.Tax = (pricing.Total / 100) * request.Vat;
                pricing.GrandTotal = (pricing.Total + pricing.Tax);
                if(room.Place.GP != 0)
                {
                    pricing.SubTotal = (pricing.GrandTotal / 100) * room.Place.GP;
                    pricing.HeadTotal = pricing.GrandTotal - pricing.SubTotal;
                }

                cmdResult.Status = true;
                cmdResult.Data = pricing;
                cmdResult.Message = request.Language == "EN" ? "Calculate Pricing Success" : "คำนวณราคาสำเร็จ";
            }
            catch (Exception e)
            {
                cmdResult.Message = e.Message;
            }

            return await Task.FromResult(cmdResult);
        }
    }
}