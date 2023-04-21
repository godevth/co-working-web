using System.Collections.Generic;

namespace CoreCMS.Data
{
    public class CowConfig
    {
        public int CheckInTime { get; set; }
        public int CancelBookingTime { get; set; }
        public int EditBookingTime { get; set; }
        public string GooglePlayUrl { get; set; }
        public string AppleStoreUrl { get; set; }
        public int CheckOutTime { get; set; }
        public string BatchUserId { get; set; }
        public string PaymentSecret { get; set; }
        public List<string> BookingStatusColor { get; set; }
        public List<string> BookingStatusIcon { get; set; }

        public Dictionary<string,string> BookingStatusColorDict 
        {
            //Status|Color
            get {
                Dictionary<string,string> dicts = new Dictionary<string, string>();
                if(BookingStatusColor != null && BookingStatusColor.Count > 0)
                {
                    foreach(string s in BookingStatusColor)
                    {
                        string[] temp = s.Split("|");
                        if(temp.Length == 2)
                            dicts.Add(temp[0], temp[1]);
                    }
                }
                return dicts;
            }
        }

        public Dictionary<string,string> BookingStatusIconDict 
        {
            //Status|IconName
            get {
                Dictionary<string,string> dicts = new Dictionary<string, string>();
                if(BookingStatusIcon != null && BookingStatusIcon.Count > 0)
                {
                    foreach(string s in BookingStatusIcon)
                    {
                        string[] temp = s.Split("|");
                        if(temp.Length == 2)
                        {
                            string icon = !string.IsNullOrEmpty(temp[1]) ? temp[1] : null;
                            dicts.Add(temp[0], icon);
                        }
                    }
                }
                return dicts;
            }
        }
    }
}