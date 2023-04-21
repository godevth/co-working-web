using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Place.Models
{
    public class PlaceModels
    {
        public Guid? PlaceId { get; set; }
        public string Name_TH { get; set; }
        public string Name_EN { get; set; }
        public string Address { get; set; }
        public string TambolId { get; set; }
        public string AmpherId { get; set; }
        public string ProvinceId { get; set; }
        public string PlaceTypeId { get; set; }
        public string Zipcode { get; set; }
        public string NearBy { get; set; } 
        public string Details { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public bool InActiveStatus { get; set; }
        public string CompanyId { get; set; }
        public List<FacilityServiceItems> ServiceItems { get; set; }
        public List<ImplementationDateItems> DateItems { get; set; }
        public string Seeing { get; set; }
        public List<string> PaymentMethods { get; set; }
        public List<string> Domains { get; set; }
        public string SeeingName { get; set; }
        public List<PaymentMethodItems> PaymentMethodItems { get; set; }
        public List<Pictures> Pictures { get; set; }
        public decimal GP { get; set; }
        public string SubMerchantId { get; set; }
    }
    public class FacilityServiceItems
    {
        public int? FacilityServicesId { get; set; }
        public Guid FacilityId { get; set; }
        public string FacilityIds { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
    }

    public class ImplementationDateItems
    {
        public int? ImplementationDateId { get; set; }
        public string StartDay { get; set; }
        // public string EndDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class PaymentMethodItems
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

}
