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
    public class GetRangeTimeMonthlyQuery : IRequest<CommandResult<List<RangeTimeMonthlyModel>>>
    {
        public string RoomId { get; set; }
        public int Month { get; set; }
        public int Year { get; set;}
        public int? BookingId { get; set; }

    }
}