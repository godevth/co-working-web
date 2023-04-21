using CoreCMS.Application.PaymentResponse.Commands;
using CoreCMS.Areas.WebApis.Models;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Produces("application/json")]
    [Route("Api/{Controller}")]
    public class PaymentResponseController : BaseController
    {
        private ILogger<PaymentResponseController> _logger;

        public PaymentResponseController(ILogger<PaymentResponseController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public async Task Handle()
        {
            string responsePayload = await new StreamReader(Request.Body, Encoding.Default).ReadToEndAsync();
            Dictionary<string, string> responseJSON = JsonConvert.DeserializeObject<Dictionary<string, string>>(responsePayload);
            String responseToken = responseJSON["payload"];
            _logger.Log(LogLevel.Information, "payload", responseToken);

            await Mediator.Send(new AddPaymentResponseCommand() { Payload = responseToken });

            //return Redirect(Url.Content("~/HtmlTemplate/PaymentResponseSuccess.html"));
        }

        [HttpPost("Frontend")]
        public async Task<IActionResult > Frontend(string paymentResponse)
        {
            // string responsePayload = await new StreamReader(Request.Form).ReadToEndAsync();
            // Dictionary<string, string> responseJSON = JsonConvert.DeserializeObject<Dictionary<string, string>>(responsePayload);
            // String responseToken = responseJSON["paymentResponse"];

            //string payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(responseToken));
            byte[] data = Convert.FromBase64String(paymentResponse);
            string decodedString = Encoding.UTF8.GetString(data);
            var options = new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    var model = JsonConvert.DeserializeObject<PaymentResponseFrontendModel>(decodedString, options);

                    string contentHtml = System.IO.File.ReadAllText($"wwwroot/EmailTemplate/paymentResponse.html"); 
                    contentHtml = contentHtml.Replace("{{BookingNumber}}", model.BookingNumber);
                    contentHtml = contentHtml.Replace("{{RespDesc}}", model.RespDesc);
                    contentHtml = contentHtml.Replace("{{CodeColor}}", model.CodeColor);

                    return Content(contentHtml, "text/html", System.Text.Encoding.UTF8);
        }
    }
}
