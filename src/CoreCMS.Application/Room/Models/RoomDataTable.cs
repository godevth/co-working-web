using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Room.Models
{
    public class RoomDataTable : Datatable
    {
        public Guid Id { get; set; }
        public string RoomTypeId { get; set; }
        public string FacilityId { get; set; }
        public string[] SortBy { get; set; }

        public bool[] SortDesc { get; set; }

        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}
