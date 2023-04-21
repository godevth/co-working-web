using System;

namespace CoreCMS.Application.ImplementationDate.Models
{
    public class ImplementationDateView
    {
        public int ImplementationDateId { get; set; }
        public string StartDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Guid PlaceId { get; set; }

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

        public string[] StartTimeSplit
        { 
            get
            {
                string[] times = StartTime.Split(':');
                return times;
            }
        }

        public string[] EndTimeSplit
        { 
            get
            {
                string[] times = EndTime.Split(':');
                return times;
            }
        }
    }
}