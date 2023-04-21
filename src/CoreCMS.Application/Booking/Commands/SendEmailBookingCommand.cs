using MediatR;
using System.Collections.Generic;

namespace CoreCMS.Application.Booking.Commands
{
    public class SendEmailBookingCommand : IRequest<bool>
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
        public string PaymentMethod
        {
            get
            {
                if (PaymentMethodCode == "PAYMENT_METHOD_COD")
                {
                    return "ชำระผ่านที่หน้าร้าน";
                }
                else if (PaymentMethodCode == "PAYMENT_METHOD_ONLINE")
                {
                    return "ชำระผ่านช่องทางออนไลน์";
                }

                return "";
            }
        }
        public string HeadSubjectCode { get; set; }
        
        public string HeadSubject
        {
            get
            {
                if(HeadSubjectCode == "BOOKING")
                {
                    return "การจองของท่านได้รับการยืนยันเรียบร้อยแล้ว";
                }
                else if (HeadSubjectCode == "EDIT_BOOKING")
                {
                    return "การจองของท่านได้รับการแก้ไขเรียบร้อยแล้ว";
                }
                else if (HeadSubjectCode == "CANCEL_BOOKING")
                {
                    return "การจองของท่านได้รับการยกเลิกเรียบร้อยแล้ว";
                }

                return "";
            }
        }
        public string HeadSubjectForCompany
        {
            get
            {
                if (HeadSubjectCode == "BOOKING")
                {
                    return "สถานที่ของท่านได้มีการจองเข้ามาในระบบ";
                }
                else if (HeadSubjectCode == "EDIT_BOOKING")
                {
                    return "สถานที่ของท่านได้มีการแก้ไขการจองเข้ามาในระบบ";
                }
                else if (HeadSubjectCode == "CANCEL_BOOKING")
                {
                    return "สถานที่ของท่านได้มีการยกเลิกการจองเข้ามาในระบบ";
                }

                return "";
            }


        }
    }

    public class EmailBookingFacilities
    {
        public string FacilityName { get; set; }
        public int FacilityQty { get; set; }
        public string FacilityPrice { get; set; }
    }
}