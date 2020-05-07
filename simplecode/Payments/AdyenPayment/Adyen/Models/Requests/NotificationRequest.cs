using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AdyenPayment.Adyen.Models
{
    public class NotificationRequest
    {
        [JsonProperty("live")]
        public string Live { get; set; }

        [JsonProperty("notificationItems")]
        public List<NotificationRequestItems> NotificationItems { get; set; }
    }

    public class NotificationRequestItems
    {
        public NotificationRequestItem NotificationRequestItem { get; set; }
    }

    public class NotificationRequestItem
    {
        [JsonProperty("additionalData")]
        public AdditionalDataNotification AdditionalData { get; set; }

        [JsonProperty("amount")]
        public Amount Amount { get; set; }

        [JsonProperty("eventCode")]
        public string EventCode { get; set; }

        [JsonProperty("eventDate")]
        public DateTime EventDate { get; set; }

        [JsonProperty("merchantAccountCode")]
        public string MerchantAccountCode { get; set; }

        [JsonProperty("merchantReference")]
        public string MerchantReference { get; set; }

        [JsonProperty("operations")]
        public List<string> Operations { get; set; }

        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("pspReference")]
        public string PspReference { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("success")]
        public string Success { get; set; }
    }

    public class AdditionalDataNotification
    {
        [JsonProperty("shopperReference")]
        public string ShopperReference { get; set; }

        [JsonProperty("shopperEmail")]
        public string ShopperEmail { get; set; }

        [JsonProperty("cardBin")]
        public string CardBin { get; set; }

        [JsonProperty("authCode")]
        public string AuthCode { get; set; }

        [JsonProperty("cardSummary")]
        public string CardSummary { get; set; }

        [JsonProperty("expiryDate")]
        public string ExpiryDate { get; set; }

        [JsonProperty("cardHolderName")]
        public string CardHolderName { get; set; }

        [JsonProperty("authorisedAmountValue")]
        public string AuthorisedAmountValue { get; set; }

        [JsonProperty("authorisedAmountCurrency")]
        public string AuthorisedAmountCurrency { get; set; }

        [JsonProperty("paymentMethodVariant")]
        public string PaymentMethodVariant { get; set; }

        [JsonProperty("recurring.shopperReference")]
        public string RecurringShopperReference { get; set; }

        [JsonProperty("recurring.recurringDetailReference")]
        public string RecurringDetailReference { get; set; }
    }
}