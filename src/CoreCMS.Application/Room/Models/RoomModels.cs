using System;
using System.Collections.Generic;

namespace CoreCMS.Application.Room.Models
{
    public class RoomModels
    {
        public Guid? Id { get; set; }

        public string Name_TH { get; set; }
        public string Name_EN { get; set; }
        public string RoomTypeId { get; set; }
        public string Detail_TH { get; set;}
        public string Detail_EN { get; set;}
        public Guid PlaceId { get; set; }
        // public string Qty { get; set; }
        // public string Price { get; set; }
        public string Capacity { get; set; }
        public bool InActiveStatus { get; set; }
        public List<Pictures> Pictures { get; set; }
        public List<PriceItem> Price { get; set; }
        public decimal GP { get; set; }
        // public string Types { get; set; }
        public List<FacilityServiceItems> ServiceItems { get; set; }
    }

    public class PriceItem{
        public int? Id { get; set; }
        public string TimeType { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
    }
    public class Pictures
    {
        public int? Id { get; set; }
        public string Base64s { get; set; }
        public string ContentTypes { get; set; }
         public string DownloadFileBase64Url { get; set; }

        public string DownloadFileByteUrl { get; set; }

    }
    public class FacilityServiceItems
    {
        public int? FacilityServicesId { get; set; }
        public Guid FacilityId { get; set; }
        public string FacilityIds { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
    }
}