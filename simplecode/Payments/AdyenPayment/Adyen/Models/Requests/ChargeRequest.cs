using Newtonsoft.Json;

namespace AdyenPayment.Adyen.Models.Requests
{
    public class ChargeRequest : AdyenCommonModel
    {
        public ChargeRequest()
        {
        }

        [JsonProperty("sercurityCode")]
        public string SercurityCode { get; set; }

        [JsonProperty("holdername")]
        public string Holdername { get; set; }

        [JsonProperty("cardnumber")]
        public string Cardnumber { get; set; }

        [JsonProperty("month")]
        public string Month { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("shopperName")]
        public string ShopperName { get; set; }
    }
}