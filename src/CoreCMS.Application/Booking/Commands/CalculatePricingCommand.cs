using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared.JsonConverters;
using CoreCMS.Application.Shared.Model;
using MediatR;
using Newtonsoft.Json;

namespace CoreCMS.Application.Booking.Commands
{
    public class CalculatePricingCommand : IRequest<CommandResult<CalculatePricing>>
    {
        public CalculatePricingCommand()
        {
            Facilities = new List<FacilityForm>();
            Vat = 7;
        }

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

        public List<FacilityForm> Facilities { get; set; }

        public string Language { get; set; }

        public decimal Discount { get; set; }

        public decimal Vat { get; set; }

        public string CurrentUserId { get; set; }
    
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

        public long StartDateTimeUnix
        { 
            get
            {
                //set second = 00
                DateTime d = StartDateTime.AddSeconds(-1 * StartDateTime.Second);
                return ((DateTimeOffset)d).ToUnixTimeMilliseconds();
            }
        }

        public long EndDateTimeUnix 
        { 
            get
            {
                //set second = 00
                DateTime d = EndDateTime.AddSeconds(-1 * EndDateTime.Second);
                return ((DateTimeOffset)d).ToUnixTimeMilliseconds();
            }
        }
    }
}