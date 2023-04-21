using System;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared.JsonConverters;
using MediatR;
using Newtonsoft.Json;

namespace CoreCMS.Application.Booking.Commands
{
    public class CheckPlaceStatusCommand : IRequest<PlaceStatus>
    {
        [Required]
        public Guid RoomId { get; set; }

        public string TimeType { get; set; }

        //GMT0
        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime StartDateTime { get; set; }

        //GMT0
        [JsonConverter(typeof(UnixMillisecondsConverter))]
        [Required]
        public DateTime EndDateTime { get; set; }

        //กรณี edit booking
        public int? BookingId { get; set; }

        public DateTime StartDateTimeLocal 
        { 
            get
            {
                //set second = 00
                DateTime d = StartDateTime.AddSeconds(-1 * StartDateTime.Second);
                return d.ToLocalTime();
            }
        }

        public DateTime EndDateTimeLocal 
        { 
            get
            {
                //set second = 00
                DateTime d = EndDateTime.AddSeconds(-1 * EndDateTime.Second);
                return d.ToLocalTime();
            }
        }
    }
}