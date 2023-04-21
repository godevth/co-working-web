using System;

namespace CoreCMS.Application.TermAndCondition.Models
{
    public class PlaceConsentView
    {
        public bool IsDefault { get; set; }
        public Guid? PlaceId { get; set; }
        public string PlaceName { get; set; }
        public int? TermId { get; set; }
        public string Name { get; set; }
        public string TermAndCondition { get; set; }
    }
}