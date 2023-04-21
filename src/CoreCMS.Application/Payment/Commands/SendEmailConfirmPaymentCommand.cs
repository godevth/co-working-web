using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Booking.Commands;
using MediatR;

namespace CoreCMS.Application.Payment.Commands
{
    public class SendEmailConfirmPaymentCommand : IRequest<bool>
    {
        public string BookingEmail { get; set; }
        public string BookingForEmail { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string PlaceName { get; set; }
        public string RoomName { get; set; }
        public string PlaceAddress { get; set; }
        public string PictureUrl { get; set; }
        public string BookingQty { get; set; }
        public string CheckInDate { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutDate { get; set; }
        public string CheckOutTime { get; set; }
        public int BookingId { get; set; }
        public string BookingNo { get; set; }
        public string BookingName { get; set; }
        public string BookingForName { get; set; }
        public string RoomSeat { get; set; }
        public List<EmailBookingFacilities> Facility { get; set; }
        public string PriceTotal { get; set; }
        public string MapUrl { get; set; }
        public string BookingDate { get; set; }
        public string Remark { get; set; }
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodName { get; set; }
        public string HeadSubjectCode { get; set; }

        public string HeadSubject
        {
            get
            {
                if (HeadSubjectCode == "PAYMENT")
                {
                    return "ยืนยันการชำระเงิน";
                }
                return "";
            }
        }
    }
}