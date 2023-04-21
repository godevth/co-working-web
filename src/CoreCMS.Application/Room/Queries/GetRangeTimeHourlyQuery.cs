using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Room.Models;
using CoreCMS.Application.Shared.JsonConverters;
using CoreCMS.Application.Shared.Model;
using MediatR;
using Newtonsoft.Json;

namespace CoreCMS.Application.Room.Queries
{
    public class GetRangeTimeHourlyQuery : IRequest<CommandResult<List<RangeTimeHourlyModel>>>
    {
        public string RoomId { get; set; }

        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime SearchDate { get; set; }

        public DateTime SearchDateLocal 
        { 
            get
            {
                return SearchDate.ToLocalTime();
            }
        }
        public int? BookingId { get; set; }
    }
}