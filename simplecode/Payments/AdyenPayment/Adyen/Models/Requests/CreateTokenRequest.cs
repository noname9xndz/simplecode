using Newtonsoft.Json;

namespace AdyenPayment.Adyen.Models.Requests
{
    public class CreateTokenRequest : AdyenCommonModel
    {
        public CreateTokenRequest()
        {
        }

        [JsonProperty("shopperName")]
        public string ShopperName { get; set; }

        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }

        [JsonProperty("expiryMonth")]
        public string ExpiryMonth { get; set; }

        [JsonProperty("expiryYear")]
        public string ExpiryYear { get; set; }

        [JsonProperty("securityCode")]
        public string SecurityCode { get; set; }

        [JsonProperty("holderName")]
        public string HolderName { get; set; }

        [JsonProperty("clientID")]
        public string ClientID { get; set; }
    }
}