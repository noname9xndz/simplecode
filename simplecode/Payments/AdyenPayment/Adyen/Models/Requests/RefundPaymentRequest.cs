using Newtonsoft.Json;

namespace AdyenPayment.Adyen.Models.Requests
{
    public class RefundPaymentRequest : AdyenCommonModel
    {
        public RefundPaymentRequest()
        {
        }

        [JsonProperty("pspReference")]
        public string PspReference { get; set; }

        [JsonProperty("clientID")]
        public string ClientId { get; set; }
    }
}