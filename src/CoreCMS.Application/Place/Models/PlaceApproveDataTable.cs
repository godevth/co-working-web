using SBP.Common.Data.Datatables;


namespace CoreCMS.Application.Place.Models
{
    public class PlaceApproveDataTable : Datatable
    {
        public bool? ApproveStatus { get; set;}
        public string[] SortBy { get; set; }

        public bool[] SortDesc { get; set; }

        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}
