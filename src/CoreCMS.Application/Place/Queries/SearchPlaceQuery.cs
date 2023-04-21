using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Shared.JsonConverters;
using CoreCMS.Application.Shared.Model;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Application.Place.Queries
{
    public class SearchPlaceQuery : IRequest<ResultByGroupView>
    {
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public string SearchKeyword { get; set; }
        public string NearBy { get; set; }
        public List<int> PlaceType { get; set; }
        public List<string> Facility { get; set; }
        public int? Capacity { get; set; }
        public decimal? StartPrice { get; set; }
        public decimal? EndPrice { get; set; }
        public string ProvinceId { get; set; }
        public string AmpherId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string SearchType { get; set;}
        public string UserId { get; set; }
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
