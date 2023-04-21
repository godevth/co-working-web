using System;
using System.Collections.Generic;
using System.Text;
using CoreCMS.Application.Room.Models;

namespace CoreCMS.Application.Facility.Models
{
    public class FacilityView
    {
        public Guid Id { get; set; }
        public string Name_TH { get; set; }
        public string Name_EN { get; set; }
        public string FacilityTypeId { get; set; }
        public string FacilityTypeName { get; set; }
        public bool InActiveStatus { get; set; }
        public List<Pictures> Pictures { get; set; }

    }
}
