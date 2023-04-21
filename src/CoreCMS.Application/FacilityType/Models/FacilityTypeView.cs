using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.FacilityType.Models
{
    public class FacilityTypeView
    {
        public int Id { get; set; }

        public string Name_TH { get; set; }
        public string Name_EN { get; set; }

        public bool InActiveStatus { get; set; }

    }
}
