using Newtonsoft.Json;

namespace AdyenPayment.Adyen.Models
{
    public class CurrencyResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("point")]
        public string Point { get; set; }
    }
}