using Newtonsoft.Json;

namespace CoreCMS.Application.PaymentResponse.Models
{
    public class PaymentResponseModel
    {
        [JsonProperty("merchantID")]
        public string MerchantID { get; set; }
        [JsonProperty("invoiceNo")]
        public string BookingNumber { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("mcpAmount")]
        public decimal McpAmount { get; set; }
        [JsonProperty("mcpFxRate")]
        public decimal McpFxRate { get; set; }
        [JsonProperty("mcpCurrencyCode")]
        public string McpCurrencyCode { get; set; }
        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }
        [JsonProperty("transactionDateTime")]
        public string TransactionDateTime { get; set; }
        [JsonProperty("agentCode")]
        public string AgentCode { get; set; }
        [JsonProperty("channelCode")]
        public string ChannelCode { get; set; }
        [JsonProperty("channelCodeName")]
        public string ChannelCodeName { get; set; }
        [JsonProperty("approvalCode")]
        public string ApprovalCode { get; set; }
        [JsonProperty("referenceNo")]
        public string ReferenceNo { get; set; }
        [JsonProperty("pan")]
        public string Pan { get; set; }
        [JsonProperty("cardToken")]
        public string CardToken { get; set; }
        [JsonProperty("issuerCountry")]
        public string IssuerCountry { get; set; }
        [JsonProperty("eci")]
        public string Eci { get; set; }
        [JsonProperty("installmentPeriod")]
        public string InstallmentPeriod { get; set; }
        [JsonProperty("interestType")]
        public string InterestType { get; set; }
        [JsonProperty("interestRate")]
        public decimal InterestRate { get; set; }
        [JsonProperty("installmentMerchantAbsorbRate")]
        public decimal InstallmentMerchantAbsorbRate { get; set; }
        [JsonProperty("recurringUniqueID")]
        public string RecurringUniqueID { get; set; }
        [JsonProperty("fxAmount")]
        public decimal FxAmount { get; set; }
        [JsonProperty("fxRate")]
        public decimal FxRate { get; set; }
        [JsonProperty("fxCurrencyCode")]
        public string FxCurrencyCode { get; set; }
        [JsonProperty("userDefined1")]
        public string UserDefined1 { get; set; }
        [JsonProperty("userDefined2")]
        public string UserDefined2 { get; set; }
        [JsonProperty("userDefined3")]
        public string UserDefined3 { get; set; }
        [JsonProperty("userDefined4")]
        public string UserDefined4 { get; set; }
        [JsonProperty("userDefined5")]
        public string UserDefined5 { get; set; }
        [JsonProperty("respCode")]
        public string RespCode { get; set; }
        [JsonProperty("respDesc")]
        public string RespDesc { get; set; }
    }
}
