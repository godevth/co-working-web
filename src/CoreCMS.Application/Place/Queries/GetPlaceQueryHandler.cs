using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Place.Models;
using System.Collections.Generic;
using System.Linq;
using CoreCMS.Data;
using Microsoft.Extensions.Options;
using System;
using CoreCMS.Domain;
using CoreCMS.Application.Place.Commands;
using MediatR;
using CoreCMS.Application.Booking.Commands;
using SBP.Application.ApiMasterData.Queries;
using CoreCMS.Application.ApiMasterData.Queries;
using Newtonsoft.Json;

namespace CoreCMS.Application.Place.Queries
{
    public class GetPlaceQueryHandler : BaseDbContextHandler<GetPlaceQuery, PlaceDetailViewModel>
    {
        private readonly IdentityServerConfig _config;
        protected IMediator _mediator;
        public GetPlaceQueryHandler(ApplicationDbContext context, IOptions<IdentityServerConfig> config,IMediator mediator) : base(context)
        {
            _config = config.Value;
             _mediator = mediator;
        }

        public override async Task<PlaceDetailViewModel> Handle(GetPlaceQuery request, CancellationToken cancellationToken)
        {
            var result = new PlaceDetailViewModel();

            try
            { 
                var query = _context.Place.Where(o => !o.IsDeleted && o.PlaceId.ToString() == request.PlaceId);
                result = query
                .Select(o => new PlaceDetailViewModel
                {
                    IsWishlist = _context.WishlistUserMapping.Where(i => i.PlaceId == o.PlaceId && i.UserId == request.UserId).Select(i => i.IsWishlist).FirstOrDefault(),
                    PlaceId = o.PlaceId.ToString(),
                    PlaceName = request.Language == "EN" ? o.PlaceName_EN : o.PlaceName_TH,
                    Facility = o.FacilityServices.Where(i => !i.IsDeleted && !i.InActiveStatus).Select(i => new FacilityModel()
                    {
                        FacilityId = i.FacilityId.ToString(),
                        FacilityName = request.Language == "EN" ? i.Facility.FacilityName_EN : i.Facility.FacilityName_TH,
                        Picture = _context.Picture.Where(x => x.TypeRef == "Facility" & !x.IsDeleted && !x.InActiveStatus && x.CodeRef == i.FacilityId.ToString())
                        .Select(o => new Pictures()
                        {
                            Id = o.PictureId,
                            DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", o.FileInfoId),
                            DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", o.FileInfoId)
                        }).ToList(),
                    }).ToList(),
                    PlaceType = request.Language == "EN" ? o.PlaceType.PlaceTypeNameEN != null ? o.PlaceType.PlaceTypeNameEN:o.PlaceType.PlaceTypeName :o.PlaceType.PlaceTypeName,
                    Address = o.Address,
                    Seat = o.Room.Max(m => m.Capacity),
                    Latitude = o.Latitude.ToString(),
                    Longitude = o.Longitude.ToString(),
                    NearBy = o.NearBy,
                    Picture = _context.Picture.Where(x => x.TypeRef == "Room" & !x.IsDeleted && !x.InActiveStatus && o.Room.Select(o => o.RoomId.ToString()).Contains(x.CodeRef))
                        .Select(o => new Pictures()
                        {
                            Id = o.PictureId,
                            DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", o.FileInfoId),
                            DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", o.FileInfoId)
                        }).ToList(),
                    OpenAndClose = o.ImplementationDate.Where(i => !i.IsDeleted && !i.InActiveStatus).Select(i => new OpenTimeModel()
                    {
                        Day = i.StartDay,
                        OpenTime = i.StartTime,
                        CloseTime = i.EndTime,
                        IsOpen = true
                    }).ToList(),
                    Province = o.PROVINCE_ID.ToString(),
                    Ampher = o.AMPER_ID.ToString(),
                    Tumbol = o.TAMBON_ID.ToString(),
                    Zipcode = o.ZIP_CODE,
                    SubMerchantId = o.SubMerchantId,
                    PaymentMethodItems = _context.PlacePaymentMethod.Where(o => o.PlaceId.ToString() == request.PlaceId)
                    .Select(o => new PaymentMethodItems()
                    {
                        Value = o.PaymentMethodCode,
                        Text = o.PaymentMethod.SystemVariableName
                    }).ToList()
                }).FirstOrDefault();

                var timeTypeChk = request.SearchType == "Daily" ? "Day": request.SearchType == "Monthly" ? "Month": request.SearchType == "Hourly" ? "Hours":"Hours";
                #region get Room
                var room = _context.Room.Where(o => o.PlaceId.ToString() == result.PlaceId && !o.IsDeleted && !o.InActiveStatus).Select( o => new WorkSpaceRoomViewModel()
                {
                    RoomId = o.RoomId.ToString(),
                    Seat = o.Capacity,
                    RoomName = request.Language == "EN"? o.RoomName_EN : o.RoomName_TH,
                    Detail = request.Language == "EN"? o.Detail_EN : o.Detail_TH,
                    RoomType = request.Language == "EN"? o.RoomType.RoomTypeNameEN : o.RoomType.RoomTypeName,
                    RoomTypeId =o.RoomType.RoomTypeID.ToString(),
                    IsOpen = true,
                    Item = o.RoomPrice.Where(i => !i.IsDeleted && !i.InActiveStatus && i.RoomId == o.RoomId && i.TimeType == timeTypeChk).Select( i => new RoomItem()
                    {
                        Id = i.Id,
                        TimeType = i.TimeType,
                        Qty =i.Qty,
                        Price = request.Currency == "THB" ? i.Price : i.Price/30
                    }).ToList(),
                    Price = o.RoomPrice.Where(i => !i.IsDeleted && !i.InActiveStatus && i.RoomId == o.RoomId && i.TimeType == timeTypeChk).Select(i => request.Currency == "THB" ? i.Price : i.Price/30).Min(),
                    TimeType = o.RoomPrice.Where(i => !i.IsDeleted && !i.InActiveStatus && i.RoomId == o.RoomId && i.TimeType == timeTypeChk).OrderBy(i => i.Price).Select(i => i.TimeType).FirstOrDefault(),
                    Qty = o.RoomPrice.Where(i => !i.IsDeleted && !i.InActiveStatus).OrderBy(i => i.Price).Select(i => i.Qty).FirstOrDefault(),
                    Picture = _context.Picture.Where(x => x.TypeRef == "Room" & !x.IsDeleted && !x.InActiveStatus && x.CodeRef == o.RoomId.ToString())
                        .Select(o => new Pictures()
                        {
                            Id = o.PictureId,
                            DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", o.FileInfoId),
                            DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", o.FileInfoId)
                        }).ToList(),
                    
                    Facility = o.FacilityServices.Where(i => !i.IsDeleted && !i.InActiveStatus).Select(i => new FacilityModel()
                    {
                        FacilityId = i.FacilityId.ToString(),
                        FacilityName = request.Language == "EN" ? i.Facility.FacilityName_EN : i.Facility.FacilityName_TH,
                        Picture = _context.Picture.Where(x => x.TypeRef == "Facility" & !x.IsDeleted && !x.InActiveStatus && x.CodeRef == i.FacilityId.ToString())
                        .Select(o => new Pictures()
                        {
                            Id = o.PictureId,
                            DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", o.FileInfoId),
                            DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", o.FileInfoId)
                        }).ToList(),
                    }).ToList()
                    
                }).ToList();

                room = room.Where(o => o.Item.Count() != 0).ToList();

                if(request.Capacity.HasValue)
                {
                    room = room.Where(o => o.Seat >= request.Capacity.Value).ToList();
                }

                 // Filter Room Type
                if(request.RoomTypeId.Any())
                {
                    room = room.Where(o => request.RoomTypeId.Contains(o.RoomTypeId)).ToList();
                }

                foreach( var item in room)  
                {
                    #region Check Available Room
                    DateTime startDate = new DateTime();
                    DateTime endDate = new DateTime();
                    if(request.SearchType == "Hourly")
                    {
                        startDate = request.HourlyStartLocal.Value;
                        endDate = request.HourlyEndLocal.Value;
                    }
                    else if(request.SearchType == "Daily")
                    {
                        
                        // startDate = request.DailyStartLocal.Value;
                        // endDate = request.DailyEndLocal.Value ;
                        startDate = new DateTime(request.DailyStartLocal.Value.Year, request.DailyStartLocal.Value.Month, request.DailyStartLocal.Value.Day, 17, 0, 0).AddDays(-1);
                        endDate = new DateTime(request.DailyEndLocal.Value.Year, request.DailyEndLocal.Value.Month, request.DailyEndLocal.Value.Day, 17, 0, 0).AddDays(-1);
      
                    }
                    else if(request.SearchType == "Monthly")
                    {
                        // startDate = request.MonthlyStartLocal.Value;
                        // endDate =  request.MonthlyEndLocal.Value;

                        startDate = new DateTime(request.MonthlyStartLocal.Value.Year, request.MonthlyStartLocal.Value.Month, request.MonthlyStartLocal.Value.Day, 17, 0, 0).AddDays(-1);
                        endDate = new DateTime(request.MonthlyStartLocal.Value.Year, request.MonthlyStartLocal.Value.Month, request.MonthlyStartLocal.Value.Day, 17, 0, 0).AddDays(-1);
                    }
                    else
                    {
                        throw new JsonSerializationException("กรุณาระบุช่วงเวลา"); 
                    }
                    
                    CheckPlaceStatusCommand placeStatusCommand = new CheckPlaceStatusCommand()
                    {
                        RoomId = Guid.Parse(item.RoomId),
                        TimeType = request.SearchType,
                        StartDateTime = startDate,
                        EndDateTime = endDate
                    };
                    var status = await _mediator.Send(placeStatusCommand);
                    item.IsOpen = status.IsOpen;
                    item.RoomStatus = status.IsAvailable ? "Avaliable" : "Reserve";

                    #endregion

                        foreach(var itemfac in item.Facility)
                        {
                            var chkFacility = result.Facility.Where(o => o.FacilityId == itemfac.FacilityId).Any();
                            if(!chkFacility)
                            {
                                result.Facility.Add(itemfac);
                            }
                        }
                        
                    item.RoomIcon = _context.Picture.Where(x => x.TypeRef == PictureTypeRef.RoomType && !x.IsDeleted && x.CodeRef == item.RoomTypeId)
                        .Select(i => new Pictures()
                        {
                            Id = i.PictureId,
                            DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", i.FileInfoId),
                            DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", i.FileInfoId)
                        }).FirstOrDefault(); 
                }
                #endregion

                #region Day Name
                string[] days = {"Monday", "Tuesday", "Wednesday", "Thursday","Friday","Saturday","Sunday"};
                foreach(var item in days)
                {
                    if(!result.OpenAndClose.Where(o => o.Day.Contains(item)).Any())
                    {
                        OpenTimeModel open = new OpenTimeModel()
                        {
                            Day = item ,
                            IsOpen = false
                        };
                        result.OpenAndClose.Add(open);
                    }
                }
                foreach(var item in result.OpenAndClose)
                {
                    if(item.Day == "Sunday")
                        item.Index = 0;
                    if(item.Day == "Monday")
                        item.Index = 1;
                    if(item.Day == "Tuesday")
                        item.Index = 2;
                    if(item.Day == "Wednesday")
                        item.Index = 3;
                    if(item.Day == "Thursday")
                        item.Index = 4;
                    if(item.Day == "Friday")
                        item.Index = 5;
                    if(item.Day == "Saturday")
                        item.Index = 6;
                //     item.Day = request.Language == "TH"? item.Day == "Monday" ? "วันจันทร์" : 
                //                                             item.Day == "Tuesday" ? "วันอังคาร" :
                //                                             item.Day == "Wednesday" ? "วันพุธ" : 
                //                                             item.Day == "Thursday" ? "วันพฤหัสบดี" : 
                //                                             item.Day == "Friday" ? "วันศุกร์" : 
                //                                             item.Day == "Saturday" ? "วันเสาร์" :   
                //                                             item.Day == "Sunday" ? "วันอาทิตย์" : item.Day : item.Day;
                }

                result.OpenAndClose = result.OpenAndClose.OrderBy(o => o.Index).ToList();
                #endregion

                #region  Address
                var province = await _mediator.Send(new SearchMasterProvinceQuery(){ ProvinceId = result.Province});
                result.Province = request.Language == "EN"? province.Data[0].ProvinceEN:province.Data[0].ProvinceTH;

                var ampher = await _mediator.Send(new SearchMasterAmpherQuery(){ AmpherId = result.Ampher});
                result.Ampher = request.Language == "EN"? ampher.Data[0].AmpherEN:ampher.Data[0].AmpherTH;

                var tumbol = await _mediator.Send(new SearchMasterTumbolQuery(){ TumbolId = result.Tumbol});
                result.Tumbol = request.Language == "EN"? tumbol.Data[0].TumbolEN:tumbol.Data[0].TumbolTH;
                #endregion
                
                var price = room.Where(i => i.IsOpen).Count() == 0 ? null : (decimal?)room.Where(i => i.IsOpen).Select(i => i.Price).Min();
                var timeType = room.Where(i => i.Price == price).Select(i => i.TimeType).FirstOrDefault();
                result.StartingPrice = request.Currency == "THB" ? "฿"+price+"/"+timeType : "$"+price+"/"+timeType;

                #region  Group By Room
                var groupByRooms = room.GroupBy(o =>new{o.RoomTypeId ,o.RoomType}).Select(o => new GroupByRoomTypeModel()
                {
                    RoomTypeId = o.Key.RoomTypeId,
                    RoomTypeName = o.Key.RoomType,
                    Availability = room.Where(i => i.RoomStatus == "Avaliable" &&  i.IsOpen == true && i.RoomTypeId == o.Key.RoomTypeId).Count().ToString() +"/"+room.Where(i => i.RoomTypeId == o.Key.RoomTypeId).Count().ToString(),
                    RoomTypeIcon = _context.Picture.Where(x => x.TypeRef == PictureTypeRef.RoomType && !x.IsDeleted && x.CodeRef == o.Key.RoomTypeId)
                        .Select(i => new Pictures()
                        {
                            Id = i.PictureId,
                            DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", i.FileInfoId),
                            DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", i.FileInfoId)
                        }).FirstOrDefault(),
                    Rooms = request.RoomStatus != ""? o.Where(i =>i.RoomStatus == request.RoomStatus).Select( i => i).ToList() : o.Select( i => i).ToList(),
                    
                }).ToList();
                #endregion

                result.WorkSpaceRoom = groupByRooms;  
            }
            catch(Exception e)
            {
                throw e;
            }
            return await Task.FromResult<PlaceDetailViewModel>(result);
        }
    }
}
