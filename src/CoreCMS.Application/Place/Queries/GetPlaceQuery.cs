using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Shared.JsonConverters;
using MediatR;
using Newtonsoft.Json;

namespace CoreCMS.Application.Place.Queries
{
    public class GetPlaceQuery : IRequest<PlaceDetailViewModel>
    {
        public string PlaceId { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public string BookingType { get; set; }
        public string RoomStatus { get; set;}
        public int? Capacity { get; set;}
        public string UserId { get; set;}
        public List<string> RoomTypeId { get; set;}

        public string SearchType { get; set;}
        //GMT0
        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime? HourlyStart { get; set; }
        //GMT0
        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime? HourlyEnd { get; set; }

        public DateTime? HourlyStartLocal 
        { 
            get
            {
                return HourlyStart?.ToLocalTime();
            }
        }

        public DateTime? HourlyEndLocal 
        { 
            get
            {
                return HourlyEnd?.ToLocalTime();
            }
        }

        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime? DailyStart { get; set; }
        //GMT0
        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime? DailyEnd { get; set; }

        public DateTime? DailyStartLocal 
        { 
            get
            {
                return DailyStart?.ToLocalTime();
            }
        }

        public DateTime? DailyEndLocal 
        { 
            get
            {
                return DailyEnd?.ToLocalTime();
            }
        }
        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime? MonthlyStart { get; set; }
        //GMT0
        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime? MonthlyEnd { get; set; }

        public DateTime? MonthlyStartLocal 
        { 
            get
            {
                return MonthlyStart?.ToLocalTime();
            }
        }

        public DateTime? MonthlyEndLocal 
        { 
            get
            {
                return MonthlyEnd?.ToLocalTime();
            }
        }

        
    }
}
