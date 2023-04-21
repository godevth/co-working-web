using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using CoreCMS.Application.Shared.JsonConverters;

namespace CoreCMS.Application.Booking.Models
{
   public class HistoryView
    {
        public int BookingId { get; set; }
        public int BookingRunning { get; set; }
        public string BookingNumber { get; set; }
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodName { get; set; }
        public string Remark { get; set; }
        public bool IsOwner { get; set; }
        public string CustomerId { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public string BookingStatusCode { get; set; }
        public string BookingStatusName { get; set; } 
        public string BookingStatusColor { get; set; }
        public string BookingStatusIconName { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public string OwnerId { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string OwnerPhoneCountryCode { get; set; }
        public bool IsDeleted { get; set; }
        public bool InActiveStatus { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public string PriceTimeType { get; set; }
        public int PriceQty { get; set; }
        public decimal Price { get; set; }
        public Guid RoomId { get; set; }
        public string RoomNameTH { get; set; }
        public string RoomNameEN { get; set; }
        public int RoomCapacity { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public Guid PlaceId { get; set; }
        public string PlaceNameTH { get; set; }
        public string PlaceNameEN { get; set; }
        public string PlaceAddress { get; set; }
        public float PlaceLatitude { get; set; }
        public float PlaceLongitude { get; set; }
        public int? PlaceTambonId { get; set; }
        public int? PlaceAmperId { get; set; }
        public int? PlaceProvinceId { get; set; }
        public string PlaceZipCode { get; set; }
        public string PlaceDetail { get; set; }
        public string PlaceNearBy { get; set; }
        public int PlaceTypeId { get; set; }
        public string PlaceTypeName { get; set; }
        public bool IsCheckIn { get; set; }
        public bool IsCheckOut { get; set; }
        public bool IsCancel { get; set; }
        public bool IsRefund { get; set; }

        public string BookingStartDateString
        {
            get
            {
                return BookingStartDate.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public string BookingEndDateString
        {
            get
            {
                return BookingEndDate.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public string CustomerName
        {
            get
            {
                return $"{CustomerFirstname} {CustomerLastname}";
            }
        }
    }
}
