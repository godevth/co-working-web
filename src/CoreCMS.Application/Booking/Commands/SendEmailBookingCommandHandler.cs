using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Office365.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.Booking.Commands
{
    public class SendEmailBookingCommandHandler : BaseDbContextWithMediatorHandler<SendEmailBookingCommand, bool>
    {
        private readonly IGraphProvider _microsoftGraphProvider;
        private readonly IdentityServerConfig _config;

        public SendEmailBookingCommandHandler(ApplicationDbContext context, IMediator mediator,
            IGraphProvider microsoftGraphProvider, IOptions<IdentityServerConfig> config) : base(context, mediator)
        {
            _microsoftGraphProvider = microsoftGraphProvider;
            _config = config.Value;
        }

        public override async Task<bool> Handle(SendEmailBookingCommand request, CancellationToken cancellationToken)
        {
            bool status = false;

            try
            {
                string templateFacility = "";
                string templateFacility2 = "";
                string pictureUrl = "";

                if (request.Facility.Any())
                {
                    string pathFacility = $"wwwroot/EmailTemplate/FacilityList.html";
                    string pathFacility2 = $"wwwroot/EmailTemplate/FacilityList2.html";
                    var templateFacilityList = new List<string>();
                    var templateFacilityList2 = new List<string>();
                    foreach (var item in request.Facility)
                    {
                        string fa = System.IO.File.ReadAllText(pathFacility);
                        fa = fa.Replace("{{facilityName}}", item.FacilityName);
                        fa = fa.Replace("{{facilityPrice}}", item.FacilityPrice);

                        string fa2 = System.IO.File.ReadAllText(pathFacility2);
                        fa2 = fa2.Replace("{{facilityName}}", item.FacilityName);
                        fa2 = fa2.Replace("{{facilityQty}}", item.FacilityQty.ToString());

                        templateFacilityList.Add(fa);
                        templateFacilityList2.Add(fa2);
                    }

                    templateFacility = string.Join(string.Empty, templateFacilityList);
                    templateFacility2 = string.Join(string.Empty, templateFacilityList2);
                }

                if (!string.IsNullOrEmpty(request.PictureUrl))
                {
                    pictureUrl = string.Format("{0}/Mobile/Api/DownloadFile/{1}?FileInfoId={2}", _config.Authority, "Byte", request.PictureUrl);
                }

                #region  SendEmail Customer
                string pathCustomer = $"wwwroot/EmailTemplate/Booking(Customer).html";


                string contentCustomer = System.IO.File.ReadAllText(pathCustomer);
                contentCustomer = contentCustomer.Replace("{{dearTo}}", request.BookingName);
                contentCustomer = contentCustomer.Replace("{{bookingType}}", request.HeadSubject);
                contentCustomer = contentCustomer.Replace("{{bookingNo}}", request.BookingNo);
                contentCustomer = contentCustomer.Replace("{{placeName}}", request.PlaceName);
                contentCustomer = contentCustomer.Replace("{{roomName}}", request.RoomName);
                contentCustomer = contentCustomer.Replace("{{placeAddress}}", request.PlaceAddress);
                contentCustomer = contentCustomer.Replace("{{bookingQty}}", request.BookingQty);
                contentCustomer = contentCustomer.Replace("{{pictureUrl}}", string.Format("src='{0}'", pictureUrl));
                contentCustomer = contentCustomer.Replace("{{checkInDate}}", request.CheckInDate);
                contentCustomer = contentCustomer.Replace("{{checkInTime}}", request.CheckInTime);
                contentCustomer = contentCustomer.Replace("{{checkOutDate}}", request.CheckOutDate);
                contentCustomer = contentCustomer.Replace("{{checkOutTime}}", request.CheckOutTime);
                contentCustomer = contentCustomer.Replace("{{bookingName}}", request.BookingName);
                contentCustomer = contentCustomer.Replace("{{bookingForName}}", request.BookingForName);
                contentCustomer = contentCustomer.Replace("{{roomSeat}}", string.Format("{0} {1}", request.RoomName, request.RoomSeat));
                contentCustomer = contentCustomer.Replace("{{facilityList}}", templateFacility);
                contentCustomer = contentCustomer.Replace("{{facilityList2}}", templateFacility2);
                contentCustomer = contentCustomer.Replace("{{priceTotal}}", request.PriceTotal);
                contentCustomer = contentCustomer.Replace("{{mapUrl}}", request.MapUrl);
                contentCustomer = contentCustomer.Replace("{{bookingDate}}", request.BookingDate);
                contentCustomer = contentCustomer.Replace("{{noOfSeat}}", request.RoomSeat);
                contentCustomer = contentCustomer.Replace("{{remark}}", request.Remark);
                contentCustomer = contentCustomer.Replace("{{paymentMethod}}", request.PaymentMethod);
                //sendto booking email
                await _microsoftGraphProvider.SendEmail(string.Join(',', request.BookingEmail), request.HeadSubject, contentCustomer);

                //send to booking for email
                if (!string.IsNullOrEmpty(request.BookingForEmail))
                {
                    contentCustomer = System.IO.File.ReadAllText(pathCustomer);
                    contentCustomer = contentCustomer.Replace("{{dearTo}}", request.BookingForName);
                    contentCustomer = contentCustomer.Replace("{{bookingType}}", request.HeadSubject);
                    contentCustomer = contentCustomer.Replace("{{bookingNo}}", request.BookingNo);
                    contentCustomer = contentCustomer.Replace("{{placeName}}", request.PlaceName);
                    contentCustomer = contentCustomer.Replace("{{roomName}}", request.RoomName);
                    contentCustomer = contentCustomer.Replace("{{placeAddress}}", request.PlaceAddress);
                    contentCustomer = contentCustomer.Replace("{{bookingQty}}", request.BookingQty);
                    contentCustomer = contentCustomer.Replace("{{pictureUrl}}", string.Format("src='{0}'", pictureUrl));
                    contentCustomer = contentCustomer.Replace("{{checkInDate}}", request.CheckInDate);
                    contentCustomer = contentCustomer.Replace("{{checkInTime}}", request.CheckInTime);
                    contentCustomer = contentCustomer.Replace("{{checkOutDate}}", request.CheckOutDate);
                    contentCustomer = contentCustomer.Replace("{{checkOutTime}}", request.CheckOutTime);
                    contentCustomer = contentCustomer.Replace("{{bookingName}}", request.BookingName);
                    contentCustomer = contentCustomer.Replace("{{bookingForName}}", request.BookingForName);
                    contentCustomer = contentCustomer.Replace("{{roomSeat}}", string.Format("{0} {1}", request.RoomName, request.RoomSeat));
                    contentCustomer = contentCustomer.Replace("{{facilityList}}", templateFacility);
                    contentCustomer = contentCustomer.Replace("{{facilityList2}}", templateFacility2);
                    contentCustomer = contentCustomer.Replace("{{priceTotal}}", request.PriceTotal);
                    contentCustomer = contentCustomer.Replace("{{mapUrl}}", request.MapUrl);
                    contentCustomer = contentCustomer.Replace("{{bookingDate}}", request.BookingDate);
                    contentCustomer = contentCustomer.Replace("{{noOfSeat}}", request.RoomSeat);
                    contentCustomer = contentCustomer.Replace("{{remark}}", request.Remark);
                    await _microsoftGraphProvider.SendEmail(string.Join(',', request.BookingForEmail), request.HeadSubject, contentCustomer);

                }
                #endregion

                #region  SendEmail Company
                string pathCompany = $"wwwroot/EmailTemplate/Booking(Campany).html";

                string contentCompany = System.IO.File.ReadAllText(pathCompany);
                contentCompany = contentCompany.Replace("{{bookingTypeForCompany}}", request.HeadSubjectForCompany);
                contentCompany = contentCompany.Replace("{{companyName}}", request.CompanyName);
                contentCompany = contentCompany.Replace("{{bookingNo}}", request.BookingNo);
                contentCompany = contentCompany.Replace("{{urlBooking}}", string.Format("{0}{1}{2}", _config.Authority, "/Booking/Detail/", request.BookingId));
                contentCompany = contentCompany.Replace("{{placeName}}", request.PlaceName);
                contentCompany = contentCompany.Replace("{{roomName}}", request.RoomName);
                contentCompany = contentCompany.Replace("{{placeAddress}}", request.PlaceAddress);
                contentCompany = contentCompany.Replace("{{bookingQty}}", request.BookingQty);
                contentCompany = contentCompany.Replace("{{pictureUrl}}", string.Format("src='{0}'", pictureUrl));
                contentCompany = contentCompany.Replace("{{checkInDate}}", request.CheckInDate);
                contentCompany = contentCompany.Replace("{{checkInTime}}", request.CheckInTime);
                contentCompany = contentCompany.Replace("{{checkOutDate}}", request.CheckOutDate);
                contentCompany = contentCompany.Replace("{{checkOutTime}}", request.CheckOutTime);
                contentCompany = contentCompany.Replace("{{bookingName}}", request.BookingName);
                contentCompany = contentCompany.Replace("{{bookingForName}}", request.BookingForName);
                contentCompany = contentCompany.Replace("{{roomSeat}}", string.Format("{0} {1}", request.RoomName, request.RoomSeat));
                contentCompany = contentCompany.Replace("{{facilityList}}", templateFacility);
                contentCompany = contentCompany.Replace("{{facilityList2}}", templateFacility2);
                contentCompany = contentCompany.Replace("{{priceTotal}}", request.PriceTotal);
                contentCompany = contentCompany.Replace("{{mapUrl}}", request.MapUrl);
                contentCompany = contentCompany.Replace("{{bookingDate}}", request.BookingDate);
                contentCompany = contentCompany.Replace("{{noOfSeat}}", request.RoomSeat);
                contentCompany = contentCompany.Replace("{{remark}}", request.Remark);
                contentCompany = contentCompany.Replace("{{paymentMethod}}", request.PaymentMethod);
                await _microsoftGraphProvider.SendEmail(request.CompanyEmail, request.HeadSubjectForCompany, contentCompany);
                #endregion

                status = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return await Task.FromResult(status);
        }
    }
}