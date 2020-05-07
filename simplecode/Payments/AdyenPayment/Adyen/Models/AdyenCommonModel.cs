using Newtonsoft.Json;

namespace AdyenPayment.Adyen.Models
{
    public class AdyenCommonModel
    {
        public AdyenCommonModel()
        {
            Amount = new Amount();
        }

        public string AdyenApiUrl { set; get; }
        public string AdyenApiKey { set; get; }

        [JsonProperty("merchantAccount")]
        public string MerchantAccount { get; set; }

        [JsonProperty("amount")]
        public Amount Amount { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class Amount
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }
    }
}