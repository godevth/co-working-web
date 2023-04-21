using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CoreCMS.Application.Place.Commands
{
    public class IsOpeningCommand : IRequest<bool>
    {
        [Required]
        public Guid PlaceId { get; set; }

        [Required]
        public string StartDay { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        public int StartTimeInt 
        { 
            get
            {
                string[] times = StartTime.Split(':');
                return (int.Parse(times[0]) * 100) + (int.Parse(times[1]));
            }
        }
        public int EndTimeInt 
        { 
            get
            {
                string[] times = EndTime.Split(':');
                return (int.Parse(times[0]) * 100) + (int.Parse(times[1]));
            }
        }
    }
}