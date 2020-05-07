using Newtonsoft.Json;
using System;

namespace AdyenPayment.Adyen.Models
{
    public class PayByLinkResponse
    {
        public PayByLinkSuccess PayByLink { get; set; }
        public PayByLinkError PayByLinkError { get; set; }

        public PayByLinkResponse()
        {
            PayByLink = new PayByLinkSuccess();
            PayByLinkError = new PayByLinkError();
        }
    }

    public class PayByLinkError
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errorType")]
        public string ErrorType { get; set; }
    }

    public class PayByLinkSuccess
    {
        [JsonProperty("amount")]
        public Amount Amount { get; set; }

        [JsonProperty("expiresAt")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}