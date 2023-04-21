using System;

namespace CoreCMS.Application.Booking.Models
{
    public class PlaceRoomView
    {
        public Guid RoomId { get; set; }
        public string RoomNameTH { get; set; }
        public string RoomNameEN { get; set; }
        public int RoomCapacity { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public Guid PlaceId { get; set; }
        public string PlaceNameTH { get; set; }
        public string PlaceNameEN { get; set; }
        public string PlaceAddress { get; set; }
        public float PlaceLatitude { get; set; }
        public float PlaceLongitude { get; set; }
        public int? PlaceTambonId { get; set; }
        public int? PlaceAmperId { get; set; }
        public int? PlaceProvinceId { get; set; }
        public string PlaceZipCode { get; set; }
        public string PlaceDetail { get; set; }
        public string PlaceNearBy { get; set; }
        public int PlaceTypeId { get; set; }
        public string PlaceTypeName { get; set; }
    }
}