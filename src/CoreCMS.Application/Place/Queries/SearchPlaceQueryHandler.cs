using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Place.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using CoreCMS.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using CoreCMS.Domain;
using CoreCMS.Application.Privilege.Queries;
using MediatR;

namespace CoreCMS.Application.Place.Queries
{
    public class SearchPlaceQueryHandler : BaseDbContextWithMediatorHandler<SearchPlaceQuery, ResultByGroupView>
    {
        private readonly IdentityServerConfig _config;

        public SearchPlaceQueryHandler(ApplicationDbContext context, IMediator mediator, IOptions<IdentityServerConfig> config) : base(context, mediator)
        {
            _config = config.Value;
        }

        public override async Task<ResultByGroupView> Handle(SearchPlaceQuery request, CancellationToken cancellationToken)
        {

            var result = new ResultByGroupView();
 
            request.ItemsPerPage = request.ItemsPerPage > 0 ? request.ItemsPerPage : 20;
            request.Page = request.Page > 0 ? request.Page : 1;
            int page = request.Page - 1;

            var query = _context.Place.Where(o => !o.IsDeleted && !o.InActiveStatus);

            #region Get Place ตามสิทธิ์ของ User
            if(!string.IsNullOrEmpty(request.UserId))
            {
                GetPlaceByUserIdQuery getPlaceByUser = new GetPlaceByUserIdQuery()
                {
                    UserId = request.UserId
                };
                var placeByUser = await _mediator.Send(getPlaceByUser);
                if(placeByUser.IsPublic)
                {
                    query = query.Where(o => o.SeeingTypeCode == "SEEING_TYPE_PUBLIC");
                }
                else 
                {
                    //user submit consent แล้ว
                    if(placeByUser.PlaceIds.Any())
                    {
                        query = query.Where(o => placeByUser.PlaceIds.Contains(o.PlaceId));
                    }
                    //user ยังไม่ submit consent
                    else
                    {
                        //where ยังไงก็ได้ห้ามเจอค่า
                        query = query.Where(o => o.SeeingTypeCode == "5ba208f4");
                    }
                }
            }
            else 
            {
                query = query.Where(o => o.SeeingTypeCode == "SEEING_TYPE_PUBLIC");
            }
            #endregion

            #region filter SearchKeyword
            if (!string.IsNullOrEmpty(request.SearchKeyword))
            {
                
                var keywords = request.SearchKeyword.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var predicate = query;

                foreach (var kw in keywords)
                {
                    var keyword = kw.Trim();
                    query = query.Where(o =>
                            (o.PlaceName_TH.ToLower().Contains(keyword) )||
                            o.PlaceName_EN.ToLower().Contains(keyword) || o.PlaceName_TH.StartsWith(keyword) || o.PlaceName_EN.ToLower().StartsWith(keyword)
                            );
                }
            }
            // PlaceType
            if (request.PlaceType.Any())
            {
                query = query.Where(x => request.PlaceType.Contains(x.PlaceTypeID));
            }
            // Facility
            if (request.Facility.Any())
            {
                query = query.Where(x => x.FacilityServices.Any(f => request.Facility.Contains(f.FacilityId.ToString())));
            }
            // StartPrice
            if (request.StartPrice.HasValue && request.EndPrice.HasValue)
            {
                query = query.Where(x => x.Room.Any(r => r.RoomPrice.Any(r => r.Price >= request.StartPrice && r.Price <= request.EndPrice)));
            }

            //Province
            if(!string.IsNullOrEmpty(request.ProvinceId))
            {
                query = query.Where(x => x.PROVINCE_ID == int.Parse(request.ProvinceId));
            }
            // Ampher
            if(!string.IsNullOrEmpty(request.AmpherId))
            {
                query = query.Where(x => x.AMPER_ID == int.Parse(request.AmpherId));
            }

            // Near By 
            if(!string.IsNullOrEmpty(request.NearBy))
            {
                var nearByList = request.NearBy.Split(",").ToList();
                query = query.Where(x => nearByList.Contains(x.NearBy));
            }
            #endregion

            // Location
            if(!string.IsNullOrEmpty(request.Longitude) && !string.IsNullOrEmpty(request.Latitude))
            {
                var constValue = 57.2957795130823D;

                var constValue2 = 3958.75586574D;
                
                query = (from l in query
                let temp = Math.Sin(l.Latitude.Value / constValue) *  Math.Sin(Convert.ToDouble(request.Latitude) / constValue) +
                                                Math.Cos(Convert.ToDouble(l.Latitude.Value) / constValue) *
                                                Math.Cos(Convert.ToDouble(request.Latitude) / constValue) *
                                                Math.Cos((Convert.ToDouble(request.Longitude) / constValue) - (Convert.ToDouble(l.Longitude.Value) / constValue))
                                            let calMiles = (constValue2 * Math.Acos(temp > 1 ? 1 : (temp < -1 ? -1 : temp)))
                                            where (l.Latitude.Value > 0 && l.Longitude.Value > 0)
                             orderby calMiles

                select (l));
            }

            //Query Exclude BookingView
            #region
            List<string> roomPriceList = null;

            // Query Hourly
            if(request.SearchType == "Hourly")
            {
                query = query.Where(o => o.ImplementationDate.Any(i => i.StartDay == request.HourlyStartLocal.Value.DayOfWeek.ToString()));


                query = query.Where(o => o.Room.Where(i => !i.IsDeleted && !i.InActiveStatus).Any(i =>  i.RoomPrice.Any(o => !_context.BookingView
                .Where(b => b.RoomId == o.RoomId &&  request.HourlyStartLocal < b.BookingEndDate 
                && request.HourlyEndLocal > b.BookingStartDate && b.BookingStatusCode != "BOOKING_STATUS_CANCEL" )
                .Any())));

                roomPriceList = query.Select(o => o.Room.Where(i => !i.IsDeleted && !i.InActiveStatus && i.RoomPrice.Any(o => !_context.BookingView
                            .Where(b => b.RoomId == o.RoomId &&  request.HourlyStartLocal < b.BookingEndDate 
                && request.HourlyEndLocal > b.BookingStartDate && b.BookingStatusCode != "BOOKING_STATUS_CANCEL" )
                            .Any())).SelectMany(o=>o.RoomPrice).OrderBy(page=>page.Price).Select( r => r.RoomId.ToString()).FirstOrDefault()).ToList();
                
            }
            // Query Daily
            else if(request.SearchType == "Daily")
            {
                List<string> nameDay =new List<string>();
                int start = 0;
                List<DateTime> dayList = new List<DateTime>();
                var nextDay = 0;
                for(var i = request.DailyStartLocal?.Day; i <= request.DailyEndLocal?.Day ;i++)
                {
                    var day = request.DailyStartLocal?.AddDays(+nextDay).DayOfWeek.ToString();
                    nameDay.Add(day);
                    nextDay++;

                    dayList.Add( new DateTime(request.DailyStartLocal.Value.Year, request.DailyEndLocal.Value.Month, request.DailyStartLocal.Value.Day + start));
                    start++;
                }
                
                var implementationDate = _context.ImplementationDate.Where(o => !o.IsDeleted && nameDay.Contains(o.StartDay)).Select(o => new {o.PlaceId,o.StartDay}).ToList().GroupBy(o => o.StartDay).ToArray();
                var open = implementationDate[0].Select(o => o.PlaceId).Intersect(implementationDate[0].Select(o => o.PlaceId)).ToArray();
                for(var i = 1 ; i < implementationDate.Count(); i++)
                {
                    open = open.Intersect(implementationDate[i].Select(o => o.PlaceId)).ToArray();
                }
                query = query.Where(o => open.Contains(o.PlaceId) );

                List<Guid> booKing = new List<Guid>();
                foreach(var date in dayList)
                {
                  var  item = _context.BookingView.Where(o =>   o.BookingStartDate.Month == date.Month  && o.BookingStartDate.Day == date.Day
                    && o.BookingStartDate.Year == date.Year && o.BookingStatusCode == "BOOKING_STATUS_RESERVE").OrderBy(o => o.BookingStartDate).Select(o => o.RoomId).ToList();
                  
                  foreach(var i in item)
                  {
                      booKing.Add(i);
                  }
                }

                query = query.Where(o => o.Room.Where(i => !i.IsDeleted && !i.InActiveStatus).Any(i => !booKing.Contains(i.RoomId)));

                roomPriceList = query.Select(o => o.Room.Where(i => !i.IsDeleted && !i.InActiveStatus && i.RoomPrice.Any(i => !booKing.Contains(i.RoomId)))
                .SelectMany(o=>o.RoomPrice).OrderBy(page=>page.Price).Select( r => r.RoomId.ToString()).FirstOrDefault()).ToList();
            }
            // Query Monthly
            else if(request.SearchType == "Monthly")
            {
                DateTime stratDate = request.MonthlyStartLocal.Value;
                List<DateTime> dayList = new List<DateTime>();
                while (stratDate <= request.MonthlyEndLocal.Value)
                {
                    var  day = stratDate;
                    dayList.Add(day);
                    stratDate = stratDate.AddDays(1);
                }
                

                List<Guid> booKing = new List<Guid>();
                foreach(var date in dayList)
                {
                  var  item = _context.BookingView.Where(o =>   o.BookingStartDate.Month == date.Month  && o.BookingStartDate.Day == date.Day
                    && o.BookingStartDate.Year == date.Year && o.BookingStatusCode == "BOOKING_STATUS_RESERVE").OrderBy(o => o.BookingStartDate).Select(o => o.RoomId).ToList();
                  
                  foreach(var i in item)
                  {
                    booKing.Add(i);
                  }
                }
                query = query.Where(o => o.Room.Where(i => !i.IsDeleted && !i.InActiveStatus).Any(i => !booKing.Contains(i.RoomId)));

                roomPriceList = query.Select(o => o.Room.Where(i => !i.IsDeleted && !i.InActiveStatus && i.RoomPrice.Any(i => !booKing.Contains(i.RoomId)))
                .SelectMany(o=>o.RoomPrice).OrderBy(page=>page.Price).Select( r => r.RoomId.ToString()).FirstOrDefault()).ToList();
            }
            #endregion

            var count = query.Count();
            var finalQuery = query
            .Select(o => new PlaceViewModel
            {
                IsWishlist = request.UserId == null ? false :_context.WishlistUserMapping.Where(i => i.PlaceId == o.PlaceId && i.UserId == request.UserId).Select(i => i.IsWishlist).FirstOrDefault(),
                PlaceId = o.PlaceId.ToString(),
                PlaceName = request.Language == "EN" ? o.PlaceName_EN : o.PlaceName_TH,
                Currency = request.Currency,
                PriceDecimal = o.Room.Where(q => roomPriceList.Contains(q.RoomId.ToString())).SelectMany(o=>o.RoomPrice).OrderBy(page=>page.Price).Select( r => r.Price).FirstOrDefault(),
                TimeType = o.Room.Where(q => roomPriceList.Contains(q.RoomId.ToString())).SelectMany(o=>o.RoomPrice).OrderBy(page=>page.Price).Select( r => r.TimeType).FirstOrDefault(),
                Facility = request.Language == "EN" ? o.FacilityServices.Where(i => !i.IsDeleted && !i.InActiveStatus).Select(i => i.Facility.FacilityName_EN).ToList()
                    : o.FacilityServices.Where(i => !i.IsDeleted && !i.InActiveStatus).Select(i => i.Facility.FacilityName_TH).ToList(),
                PlaceType = request.Language == "EN" ? o.PlaceType.PlaceTypeNameEN != null ? o.PlaceType.PlaceTypeNameEN:o.PlaceType.PlaceTypeName :o.PlaceType.PlaceTypeName,
                Seat = o.Room.Max(m => m.Capacity),
                NearBy = o.NearBy,
                ProvinceId = o.PROVINCE_ID,
                AmpherId = o.AMPER_ID,
                Latitude = o.Latitude.ToString(),
                Longitude = o.Longitude.ToString(),
                Picture = _context.Picture.Where(x => x.TypeRef == "Room" & !x.IsDeleted && !x.InActiveStatus && o.Room.Select(o => o.RoomId.ToString()).Contains(x.CodeRef))
                    .Select(o => new Pictures()
                    {
                        Id = o.PictureId,
                        DownloadFileBase64Url = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Base64", o.FileInfoId),
                        DownloadFileByteUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", o.FileInfoId)
                    })
            });

            // ตวามจุ
            finalQuery = finalQuery.Where(o => o.Seat >= request.Capacity);

            // ราคา
            if(request.StartPrice != null && request.EndPrice != null)
            {
                finalQuery = finalQuery.Where(o => o.PriceDecimal >= request.StartPrice && o.PriceDecimal <= request.EndPrice);
            }

            var Items = await finalQuery.Skip(page * request.ItemsPerPage).Take(request.ItemsPerPage).ToArrayAsync();

            return new ResultByGroupView()
            {
                Result = new SearchMobileResult<PlaceViewModel>()
                {
                    More = (request.Page * request.ItemsPerPage) < count,
                    Total = finalQuery.Count(),
                    Items = await finalQuery.OrderBy(o => o.PlaceName).Skip(page * request.ItemsPerPage).Take(request.ItemsPerPage).ToArrayAsync()
                },
                Recommend = new SearchMobileResult<PlaceViewModel>()
                {
                    More = (request.Page * request.ItemsPerPage) < count,
                    Total = finalQuery.Count(),
                    Items = await finalQuery.Skip(page * request.ItemsPerPage).Take(request.ItemsPerPage).ToArrayAsync()
                },
                NearBy = new SearchMobileResult<PlaceViewModel>()
                {
                    More = (request.Page * request.ItemsPerPage) < count,
                    Total = finalQuery.Count(),
                    Items = await finalQuery.Skip(page * request.ItemsPerPage).Take(request.ItemsPerPage).ToArrayAsync()
                }
                // More = (request.Page * request.ItemsPerPage) < count,
                // Total = finalQuery.Count(),
                // Items = await finalQuery.Skip(page * request.ItemsPerPage).Take(request.ItemsPerPage).ToArrayAsync()
            };
        }
    }
}