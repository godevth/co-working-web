using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Place.Models
{
    public class PlaceDataTable : Datatable
    {
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string ProvinceId { get; set; }
        public string AmpherId { get; set; }
        public string PlaceTypeId { get; set; }
        public string RoomTypeId { get; set; }
        public string FacilityId { get; set; }
        public string[] SortBy { get; set; }

        public bool[] SortDesc { get; set; }

        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}
