using Newtonsoft.Json;

namespace AdyenPayment.Adyen.Models.Requests
{
    public class ChargePaymentRequest : AdyenCommonModel
    {
        public ChargePaymentRequest()
        {
        }

        [JsonProperty("shopperName")]
        public string ShopperName { get; set; }

        [JsonProperty("storedPaymentMethodId")]
        public string StoredPaymentMethodId { get; set; }

        [JsonProperty("clientID")]
        public string ClientId { get; set; }
    }
}