using CoreCMS.Application.Payment.Commands;
using CoreCMS.Application.PaymentResponse.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Persistence;
using IdentityModel;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.PaymentResponse.Commands
{
    public class AddPaymentResponseCommandHandler : BaseDbContextWithMediatorHandler<AddPaymentResponseCommand, bool>
    {
        private readonly CowConfig _config;
        public AddPaymentResponseCommandHandler(ApplicationDbContext context, IMediator mediator, IOptions<CowConfig> config) : base(context, mediator)
        {
            _config = config.Value;
        }

        public override async Task<bool> Handle(AddPaymentResponseCommand request, CancellationToken cancellationToken)
        {
            bool status = false;
            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    #region Decode JWT
                    IJsonSerializer serializer = new JsonNetSerializer();
                    var provider = new UtcDateTimeProvider();
                    IJwtValidator validator = new JwtValidator(serializer, provider);
                    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                    IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                    IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                    var payload = decoder.Decode(request.Payload, _config.PaymentSecret.ToSha256(), verify: true);

                    //Refer below Sample Decoded JWT Response
                    var options = new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    var model = JsonConvert.DeserializeObject<PaymentResponseModel>(payload, options);
                    #endregion

                    var booking = _context.Booking.Where(x => x.BookingNumber == model.BookingNumber).FirstOrDefault();
                    var data = new Domain.PaymentResponse()
                    {
                        MerchantID = model.MerchantID,
                        BookingId = booking.BookingId,
                        BookingNumber = model.BookingNumber,
                        CardToken = model.CardToken,
                        ChannelCode = model.ChannelCode,
                        ChannelCodeName = model.ChannelCodeName,
                        AgentCode = model.AgentCode,
                        ApprovalCode = model.ApprovalCode,
                        Amount = model.Amount,
                        CurrencyCode = model.CurrencyCode,
                        Eci = model.Eci,
                        FxAmount = model.FxAmount,
                        FxCurrencyCode = model.FxCurrencyCode,
                        FxRate = model.FxRate,
                        InstallmentMerchantAbsorbRate = model.InstallmentMerchantAbsorbRate,
                        InstallmentPeriod = model.InstallmentPeriod,
                        InterestRate = model.InterestRate,
                        InterestType = model.InterestType,
                        IssuerCountry = model.IssuerCountry,
                        McpAmount = model.McpAmount,
                        McpCurrencyCode = model.McpCurrencyCode,
                        McpFxRate = model.McpFxRate,
                        RecurringUniqueID = model.RecurringUniqueID,
                        Pan = model.Pan,
                        ReferenceNo = model.ReferenceNo,
                        RespCode = model.RespCode,
                        RespDesc = model.RespDesc,
                        TransactionDateTime = model.TransactionDateTime,
                        UserDefined1 = model.UserDefined1,
                        UserDefined2 = model.UserDefined2,
                        UserDefined3 = model.UserDefined3,
                        UserDefined4 = model.UserDefined4,
                        UserDefined5 = model.UserDefined5,
                        CreateDate = DateTime.Now
                    };
                    
                    //Convert toDate
                    if(!string.IsNullOrEmpty(data.TransactionDateTime))
                    {
                        DateTime outDt;
                        if(DateTime.TryParseExact(data.TransactionDateTime,
                        "yyyyMMddHHmmss",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out outDt))
                            data.TransactionDate = outDt;
                    }

                    _context.PaymentResponse.Add(data);
                    _context.SaveChanges();

                    if (!nestedTrans)
                    {
                        ts.Commit();

                        #region Payment Booking
                        if(!string.IsNullOrEmpty(data.RespCode) && data.RespCode == "0000")
                        {
                            PaymentBookingCommand paymentCommand = new PaymentBookingCommand()
                            {
                                BookingId = booking.BookingId,
                                IsOnlinePayment = true,
                                Total = booking.Remaining,
                                Paid = Convert.ToDecimal(data.Amount),
                                PaymentDate = !string.IsNullOrEmpty(data.TransactionDateTime) 
                                    ? data.TransactionDate : data.CreateDate,
                                PaymentResponseId = data.Id,
                            }; 
                            paymentCommand.Remaining = paymentCommand.Total - paymentCommand.Paid;
                            await _mediator.Send(paymentCommand);
                        }
                        #endregion
                    }

                    status = true;
                }
                catch (Exception e)
                {
                    ts.Rollback();
                }

                return await Task.FromResult(status);
            }
        }
    }
}
