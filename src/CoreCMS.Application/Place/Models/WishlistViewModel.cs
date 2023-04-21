using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoreCMS.Application.Place.Models
{
    public class WishlistViewModel
    {
        public string PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public int TambonId { get; set; }

        public string Tambon { get; set; }

        [JsonIgnore]
        public int ApmherId { get; set; }

        public string Ampher { get; set;}

        [JsonIgnore]
        public int  ProvinceId { get; set; }

        public string Province { get; set; }
        public string Zipcode { get; set; }
        public bool IsWishlist { get; set; }

        public List<Pictures> Picture { get; set;}

    }
}