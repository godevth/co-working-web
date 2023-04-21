using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Room.Models
{
    public class RoomView
    {
        public Guid Id { get; set; }

        public string Name_TH { get; set; }
        public string Name_EN { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string Facility { get; set; }
        public List<string> FacilityList { get; set; }     
        public bool InActiveStatus { get; set; }
    }

}
