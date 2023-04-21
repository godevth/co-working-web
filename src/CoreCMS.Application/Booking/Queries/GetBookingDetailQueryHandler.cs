using CoreCMS.Application.ApiMasterData.Queries;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Booking.Queries
{
    public class GetBookingDetailQueryHandler : BaseDbContextWithMediatorHandler<GetBookingDetailQuery, CommandResult<BookingDetailView>>
    {
        private readonly CowConfig _config;
        public GetBookingDetailQueryHandler(ApplicationDbContext context, IMediator mediator, IOptions<CowConfig> config) : base(context, mediator)
        {
            _config = config.Value;
        }

        public override async Task<CommandResult<BookingDetailView>> Handle(GetBookingDetailQuery request, CancellationToken cancellationToken)
        {
            CommandResult<BookingDetailView> result = new CommandResult<BookingDetailView>();
            var booking = _context.Booking.Where(o => !string.IsNullOrEmpty(request.BookingNumber) ?
                o.BookingNumber == request.BookingNumber : o.BookingId == request.BookingId)
                .Select(o => new BookingDetailView()
                {
                    BookingEndDate = o.BookingDates.Max(i => i.EndDate),
                    BookingStartDate = o.BookingDates.Min(i => i.StartDate),
                    BookingId = o.BookingId,
                    BookingNumber = o.BookingNumber,
                    CustomerEmail = o.CustomerEmail,
                    CustomerFirstname = o.CustomerFirstname,
                    CustomerLastname = o.CustomerLastname,
                    CustomerPhoneNumber = o.CustomerPhoneNumber,
                    RoomId = o.RoomId,
                    RoomName = request.Language == "TH" ? o.Room.RoomName_TH : o.Room.RoomName_EN,
                    IsOwner = o.IsOwner,
                    PaymentMethodCode = o.PaymentMethodCode,
                    PaymentMethodName = o.PaymentMethod.SystemVariableName,
                    PlaceId = o.Room.PlaceId,
                    PlaceName = request.Language == "TH" ? o.Room.Place.PlaceName_TH : o.Room.Place.PlaceName_EN,
                    Qty = o.Qty,
                    Remark = o.Remark,
                    RoomPriceId = o.RoomPriceId.Value,
                    TimeType = o.RoomPrice.TimeType,
                    BookingStatusCode = o.BookingStatusCode,
                    BookingStatusName = o.BookingStatus.SystemVariableName,
                    OwnerId = o.CreatedUserId,
                    OwnerEmail = o.CreatedUser.Email,
                    OwnerFirstName = o.CreatedUser.FirstName,
                    OwnerLastName = o.CreatedUser.LastName,
                    OwnerPhoneCountryCode = o.CreatedUser.PhoneCountryCode,
                    OwnerPhoneNumber = o.CreatedUser.PhoneNumber,
                    Capacity = o.Room.Capacity,
                    Address = o.Room.Place.Address,
                    ProvinceId = o.Room.Place.PROVINCE_ID,
                    AmpherId = o.Room.Place.AMPER_ID,
                    TambolId = o.Room.Place.TAMBON_ID,
                    Zipcode = o.Room.Place.ZIP_CODE,
                    Latitude = o.Room.Place.Latitude,
                    Longitude = o.Room.Place.Longitude,
                    GrandTotal = o.GrandTotal,
                    Total = o.Total,
                    Tax =o.Tax,
                    Discount = o.Discount,
                    RoomPrice = new RoomPriceView()
                    {
                        Id = o.RoomPriceId.Value,
                        TimeType = o.RoomPrice.TimeType,
                        Price = o.RoomPrice.Price,
                        Qty = o.RoomPrice.Qty
                    },
                    BookingFacilities = o.BookingFacilities.Select(i => new BookingFacilityView()
                    {
                        BookingFacilityId = i.BookingFacilityId,
                        FacilityServicesId = _context.FacilityServices.Where(f=>(f.RoomId ==o.RoomId || f.PlaceId == o.Room.PlaceId) && f.FacilityId==i.FacilityId && !f.IsDeleted).Select(f=>f.FacilityServicesId).FirstOrDefault(),
                        FacilityId = i.FacilityId,
                        FacilityName = request.Language == "TH" ? i.Facility.FacilityName_TH : i.Facility.FacilityName_EN,
                        Price = i.Price,
                        Qty = i.Qty,
                        IsAll = true
                    }).ToList(),
                    LastPayment = _context.Payment.Where(p => !p.IsDeleted && !p.InActiveStatus && p.BookingId == o.BookingId)
                        .Select(p => new PaymentView(){
                            PaymentMethodCode = p.PaymentMethodCode,
                            PaymentMethodName = p.PaymentMethod.SystemVariableName,
                            PaymentDate = p.PaymentDate,
                            ChannelCode = p.PaymentMethodCode == "PAYMENT_METHOD_COD" 
                                ? p.CounterPaymentCode : p.PaymentResponse.ChannelCode,
                            ChannelName = p.PaymentMethodCode == "PAYMENT_METHOD_COD" 
                                ? p.CounterPayment.SystemVariableName : p.PaymentResponse.ChannelCodeName,
                            ReferenceNo = p.PaymentMethodCode == "PAYMENT_METHOD_ONLINE" 
                                ? p.PaymentResponse.ReferenceNo : null
                        })
                        .OrderByDescending(p => p.PaymentDate)
                        .FirstOrDefault()
                }).SingleOrDefault();
            
            if(booking != null)
            {
                if(booking.ProvinceId.HasValue)
                {
                    var provinces = await _mediator.Send(new SearchMasterProvinceQuery() { });
                    booking.ProvinceName =  provinces != null ? 
                        provinces.Data.Where(p => p.ProvinceId == booking.ProvinceId.Value.ToString()).Select(p => request.Language == "TH" ? p.ProvinceTH : p.ProvinceEN).FirstOrDefault() : null;
                }

                if(booking.AmpherId.HasValue)
                {
                    var amphers = await _mediator.Send(new SearchMasterAmpherQuery() { });
                    booking.AmpherName =  amphers != null ? 
                        amphers.Data.Where(a => a.AmpherId == booking.AmpherId.Value.ToString()).Select(a => request.Language == "TH" ? a.AmpherTH : a.AmpherEN).FirstOrDefault() : null;
                }

                if(booking.TambolId.HasValue)
                {
                    var tambols = await _mediator.Send(new SearchMasterTumbolQuery() { });
                    booking.TambolName =  tambols != null ? 
                        tambols.Data.Where(t => t.TumbolId == booking.TambolId.Value.ToString()).Select(t => request.Language == "TH" ? t.TumbolTH : t.TumbolEN).FirstOrDefault() : null;
                }
                
                //Color
                var bookingStatusColorDict = _config.BookingStatusColorDict;
                booking.BookingStatusColor = bookingStatusColorDict[booking.BookingStatusCode];

                //IconName
                var bookingStatusIconDict = _config.BookingStatusIconDict;
                booking.BookingStatusIconName = bookingStatusIconDict[booking.BookingStatusCode];
            }
           result.Data = booking;
           result.Message ="ดึงข้อมูลสำเร็จ";
            
           return await Task.FromResult<CommandResult<BookingDetailView>>(result);
            
        }
    }
}
