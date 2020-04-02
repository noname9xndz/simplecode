using Newtonsoft.Json;
using System.Collections.Generic;

namespace AdyenPayment.Adyen.Models
{
    public class PaymentDetailsResponse
    {
        [JsonProperty("groups")]
        public List<groups> Groups { get; set; }

        [JsonProperty("oneClickPaymentMethods")]
        public List<oneClickPaymentMethods> OneClickPaymentMethods { get; set; }

        [JsonProperty("paymentMethods")]
        public List<paymentMethods> PaymentMethods { get; set; }

        [JsonProperty("storedPaymentMethods")]
        public List<StoredPaymentMethods> StoredPaymentMethods { get; set; }
    }

    public class groups
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }

    public class oneClickPaymentMethods
    {
        [JsonProperty("details")]
        public List<details> Details { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("recurringDetailReference")]
        public string RecurringDetailReference { get; set; }

        [JsonProperty("storedDetails")]
        public storedDetails StoredDetails { get; set; }
    }

    public class paymentMethods
    {
        [JsonProperty("brands")]
        public List<string> Brands { get; set; }

        [JsonProperty("details")]
        public List<details> Details { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class storedDetails
    {
        [JsonProperty("card")]
        public card Card { get; set; }
    }

    public class card
    {
        [JsonProperty("expiryMonth")]
        public string ExpiryMonth { get; set; }

        [JsonProperty("expiryYear")]
        public string ExpiryYear { get; set; }

        [JsonProperty("holderName")]
        public string HolderName { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }
    }

    public class details
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class StoredPaymentMethods
    {
        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("holderName")]
        public string HolderName { get; set; }

        [JsonProperty("lastFour")]
        public string LastFour { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("supportedShopperInteractions")]
        public List<string> SupportedShopperInteractions { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}