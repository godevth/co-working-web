using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreCMS.Application.Booking.Models
{
    public class BookingView : PlaceRoomView
    {
        public int BookingId { get; set; }
        public int BookingRunning { get; set; } 
        public string BookingNumber { get; set; } 
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodName { get; set; }
        public string Remark { get; set; }
        public bool IsOwner { get; set; }
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
        public DateTime CreatedDate { get; set; }
        public string OwnerId { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string OwnerPhoneCountryCode { get; set; }
        public bool IsDeleted { get; set; }
        public bool InActiveStatus { get; set; }
        public int BookingDateId { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public string OpenDay { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string PriceTimeType { get; set; }
        public int PriceQty { get; set; }
        public decimal Price { get; set; }
        public List<BookingFacilityView> BookingFacilities { get; set; }
        public List<BookingDateView> BookingDates{ get; set; }
        public string QRCode { get; set; }
        public decimal Remaining { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public decimal? LastPaymentPaid { get; set; }
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

        public string LastPaymentDateString
        {
            get
            {
                return LastPaymentDate.HasValue ? LastPaymentDate.Value.ToString("dd/MM/yyyy HH:mm") : null;
            }
        }

        public string TotalFormat
        {
            get
            {
                return string.Format("{0:n}", Total);
            }
        }

        public string DiscountFormat
        {
            get
            {
                return string.Format("{0:n}", Discount);
            }
        }

        public string TaxFormat
        {
            get
            {
                return string.Format("{0:n}", Tax);
            }
        }

        public string GrandTotalFormat
        {
            get
            {
                return string.Format("{0:n}", GrandTotal);
            }
        }

        public string PriceFormat
        {
            get
            {
                return string.Format("{0:n}", Price);
            }
        }

        public decimal PriceTotal
        {
            get
            {
                decimal total = Price * Qty;
                return total;
            }
        }

        public string PriceTotalFormat
        {
            get
            {
                return string.Format("{0:n}", PriceTotal);
            }
        }
        
        public decimal FacilityTotal
        {
            get
            {
                decimal total = 0;
                if(BookingFacilities != null && BookingFacilities.Any())
                {
                    total = BookingFacilities.Sum(o => o.Total);
                }
                return total;
            }
        }

        public string FacilityTotalFormat
        {
            get
            {
                return string.Format("{0:n}", FacilityTotal);
            }
        }
    }
}