using Newtonsoft.Json;

namespace AdyenPayment.Adyen.Models.Requests
{
    public class PaymentDetailRequests : AdyenCommonModel
    {
        public PaymentDetailRequests()
        {
        }

        [JsonProperty("shopperName")]
        public string ShopperName { set; get; }
    }
}