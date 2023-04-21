using Newtonsoft.Json;

namespace CoreCMS.Areas.WebApis.Models
{
    public class PaymentResponseFrontendModel
    {
        [JsonProperty("invoiceNo")]
        public string BookingNumber { get; set; }

        [JsonProperty("channelCode")]
        public string ChannelCode { get; set; }

        //[JsonProperty("respCode")]
        public string RespCode { get; set; }
        public string RespDesc { 
            get
            {
                if(RespCode == "2000")
                    return "Transaction completed.";
                else if (RespCode == "2001")
                    return "Transaction in progress.";
                else if (RespCode == "2002")
                    return "Transaction not found.";
                else if (RespCode == "2003")
                    return "Failed To Inquiry.";
                else if (RespCode == "2003")
                    return "Failed To Inquiry.";
                return "Transaction failed or rejected";
            }
        }
        public string CodeColor { 
            get
            {
                if(RespCode == "2000")
                    return "50af51";
                return "F76D6D";
            }
        }
    }
}