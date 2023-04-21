using System;
using System.Collections.Generic;

namespace CoreCMS.Application.Company.Models
{
    public class CompanyModels
    {
        public int? Id { get; set; }
        public bool IsEdit { get; set; }
        public string Name_TH { get; set; }
        public string Name_EN { get; set; }
        public string OwnerId { get; set; }
        public List<ProfileItem> Profile { get; set; }
    }
    public class ProfileItem{
        public int? Id { get; set; }
        public string AdminId { get; set; }
        public string PlaceId { get; set; }
    }
}