using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoreCMS.Application.Place.Models
{
    public class GroupByRoomTypeModel
    {
        public string RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public string Availability { get; set; }

        public Pictures RoomTypeIcon { get; set; }

        public List<WorkSpaceRoomViewModel> Rooms { get; set; }

    }
}