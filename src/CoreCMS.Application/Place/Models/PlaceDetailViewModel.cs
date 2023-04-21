using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoreCMS.Application.Place.Models
{
    public class PlaceDetailViewModel
    {
        public string PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsWishlist { get; set; }
        public string StartingPrice { get; set; }
        public string PlaceType { get; set; }
        public List<FacilityModel> Facility { get; set; }
        public string NearBy { get; set; }
        public int Seat { get; set; }
        public string WorkSpaceProfile { get; set; }
        public List<GroupByRoomTypeModel> WorkSpaceRoom { get; set; }
        public List<Pictures> Picture { get; set; }
        public List<OpenTimeModel> OpenAndClose { get; set; }

        public string Tumbol { get; set; }
        public string Ampher { get; set; }
        public string Province { get; set; }
        public string Zipcode { get; set;}
        public string SubMerchantId { get; set; }
        public List<PaymentMethodItems> PaymentMethodItems { get; set; }
    }

    public class Pictures
    {
        public int? Id { get; set; }
        [JsonIgnore]
        public string FileInfoId { get; set; }
        public string DownloadFileBase64Url { get; set; }

        public string DownloadFileByteUrl { get; set; }
        public string Base64s { get; set; }
        public string ContentTypes { get; set; }
        public string FileName { get; set;}

    }

    public class FacilityModel
    {
        public string FacilityId { get; set; }
        public string FacilityName { get; set; }
        public List<Pictures> Picture { get; set; }
    }

    public class OpenTimeModel
    {
        public string Day { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public int Index { get; set; }
        public bool IsOpen { get; set; }
    }
}
