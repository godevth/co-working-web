using System.Collections.Generic;
using System.Text.Json.Serialization;
using CoreCMS.Domain;

namespace CoreCMS.Application.Place.Models
{
    public class WorkSpaceRoomViewModel
    {
        public string RoomId { get; set; }
        public string RoomType { get; set; }
        public string RoomTypeId { get; set;}
        public string RoomName { get; set; }
        public string RoomStatus { get; set;}
        public string Detail { get; set; }
        public int Qty { get; set;}
        public int Seat { get; set; }
        public decimal Price { get; set; }
        public string TimeType { get; set;}
        public bool IsOpen { get; set; }
        public Pictures RoomIcon { get; set; }
        public List<RoomItem> Item { get; set; }
        public List<Pictures> Picture { get; set; }
        public List<FacilityModel> Facility { get; set; }
        
        public string TimeTypeText
        { 
            get
            {
                string text = "";
                if(TimeType == "Hours")
                    text = "HOURLY";
                else if(TimeType == "Day")
                    text = "DAILY";
                else if(TimeType == "Month")
                    text = "MONTHLY";
                return text;
            }
        }
    }

    public class RoomItem 
    {
        public int Id { get; set; }
        public string TimeType { get; set; }
        public int Qty { get; set; }
        [JsonIgnore]
        public decimal Price { get; set; }
        public string PriceString
        {
            get
            {
                return string.Format("{0:0.00}", Price);
            }
        }
        public string TimeTypeText
        { 
            get
            {
                string text = "";
                if(TimeType == "Hours")
                    text = "HOURLY";
                else if(TimeType == "Day")
                    text = "DAILY";
                else if(TimeType == "Month")
                    text = "MONTHLY";
                return text;
            }
        }
    }
}
