using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Company.Models
{
    public class CompanyView
    {
        public int Id { get; set; }

        public string Name_TH { get; set; }
        public string Name_EN { get; set; }
        public string Owner { get; set; }
        public bool InActiveStatus { get; set; }
        public bool IsEdit { get; set; }
        public List<CompanyProfileView> Profiles { get; set; }
    }
public class CompanyProfileView
    {
        public int? CompanyProfileId { get; set; }

        public string AdminId { get; set; }
        public string PlaceId { get; set; }
    }
}
