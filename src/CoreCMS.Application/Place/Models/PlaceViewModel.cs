using CoreCMS.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreCMS.Application.Place.Models
{
    public class PlaceViewModel
    {
        public string PlaceId { get; set; }
        public string PlaceName { get; set; }
        public bool IsWishlist { get; set; }
        [JsonIgnore]
        public decimal PriceDecimal { get; set; }

        [JsonIgnore]
        public string Currency { get; set; }
        [JsonIgnore]
        public string TimeType { get; set; }
        public string StringPriceDecimal
        {
            get
            {
                if(Currency == "THB")
                {
                    return string.Format("฿ {0}", PriceDecimal);
                }
                else
                {
                    return string.Format("$ {0:0.00}", PriceDecimal/30);
                }
            }
        }
        public string StartingPrice 
        { 
            get
            {
                if(Currency == "THB")
                {
                    return string.Format("฿ {0}", PriceDecimal) + "/" + TimeType;
                }
                else
                {
                    return string.Format("$ {0:0.00}", PriceDecimal/30) + "/" + TimeType ;
                }
            }
        }
        public IEnumerable<string> TypeAndFacility
        {
            get
            {
                if (Facility.Any())
                {
                    Facility.Add(PlaceType);
                    return Facility;
                }
                return new[] { PlaceType };
            }
        }
        [JsonIgnore]
        public string PlaceType { get; set; }
        [JsonIgnore]
        public List<string> Facility { get; set; }
        public string NearBy { get; set; }
        public int Seat { get; set; }
        public int? ProvinceId { get; set; }
        public int? AmpherId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }
        public IEnumerable<Pictures> Picture { get; set; }
    }
    public class PlaceTableViewModel
    {
        public string Id { get; set; }
        public string PlaceNameTH { get; set; }
        public string PlaceNameEN { get; set; }
        public string Address { get; set; }
        public string PlaceTypeName { get; set; }
        public string Facility { get; set; }
        public List<string> FacilityList { get; set; }
        public string RoomType { get; set; }
        public List<string> RoomTypeList { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceNameTH { get; set; }
        public int AmpherId { get; set; }
        public string AmpherNameTH { get; set; }
        public bool InActiveStatus { get; set; }
        public string SeeingCode { get; set; }
        public bool IsApproveCreate { get; set; }
        public bool IsApproveDelete { get; set; }
    }

}
