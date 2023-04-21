using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreCMS.Application.Place.Models
{
    public class ResultByGroupView
    {
        public SearchMobileResult<PlaceViewModel> Result { get; set;}
        public SearchMobileResult<PlaceViewModel> NearBy { get; set;}
        public SearchMobileResult<PlaceViewModel> Recommend { get; set;}

    }
}
