using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Room.Models
{
    public class RangeTimeDailyModel
    {
        public string Month { get; set;}
        public int Index { get; set; }
        public DateTime Day { get; set; }
        public long UnixMillisecond { get; set;}
        public bool IsAvailable { get; set; }
        public bool IsOpen { get; set; }
    }

}
