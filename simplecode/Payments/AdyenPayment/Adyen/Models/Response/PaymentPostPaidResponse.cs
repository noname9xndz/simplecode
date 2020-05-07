using AdyenPayment.Adyen.Enums;
using Newtonsoft.Json;

namespace AdyenPayment.Adyen.Models
{
    public class PaymentPostPaidResponse
    {
        public PaymentSuccess payment { get; set; }
        public PaymentError paymentError { get; set; }

        public PaymentPostPaidResponse()
        {
            payment = new PaymentSuccess();
            paymentError = new PaymentError();
        }
    }

    public class PaymentError
    {
        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("errorCode")]
        public string errorCode { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("errorType")]
        public string errorType { get; set; }
    }

    public class PaymentSuccess
    {
        [JsonProperty("pspReference")]
        public string pspReference { get; set; }

        [JsonProperty("resultCode")]
        public ResultCodeEnum resultCode { get; set; }

        [JsonProperty("merchantReference")]
        public string merchantReference { get; set; }

        [JsonProperty("additionalData")]
        public AdditionalData additionalData { get; set; }

        [JsonProperty("response")]
        public string response { get; set; }
    }

    public class AdditionalData
    {
        [JsonProperty("recurringProcessingModel")]
        public string RecurringProcessingModel { get; set; }

        [JsonProperty("recurring.recurringDetailReference")]
        public string RecurringDetailReference { get; set; }

        [JsonProperty("recurring.shopperReference")]
        public string ShopperReference { get; set; }

        [JsonProperty("cardHolderName")]
        public string CardHolderName { get; set; }

        [JsonProperty("cardSummary")]
        public string CardSummary { get; set; }

        [JsonProperty("expiryDate")]
        public string ExpiryDate { get; set; }

        [JsonProperty("cardBin")]
        public string CardBin { get; set; }

        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }
    }
}