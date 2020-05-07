using AdyenPayment.Adyen.Enums;
using Newtonsoft.Json;

namespace AdyenPayment.Adyen.Models
{
    public class PaymentTokenResponse
    {
        public PaymentToken PaymentToken { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class PaymentToken
    {
        [JsonProperty("pspReference")]
        public string PspReference { get; set; }

        [JsonProperty("resultCode")]
        public ResultCodeEnum? ResultCode { get; set; }

        [JsonProperty("merchantReference")]
        public string MerchantReference { get; set; }

        [JsonProperty("additionalData")]
        public AdditionalData AdditionalData { get; set; }
    }
}