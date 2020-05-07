using Newtonsoft.Json;

namespace AdyenPayment.Adyen.Models.Requests
{
    public class GeneratePaymentLinkRequest : AdyenCommonModel
    {
        public GeneratePaymentLinkRequest()
        {
            DeliveryAddress = new Address();
            BillingAddress = new Address();
        }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("shopperReference")]
        public string ShopperReference { get; set; }

        [JsonProperty("shopperEmail")]
        public string ShopperEmail { get; set; }

        [JsonProperty("shopperLocale")]
        public string ShopperLocale { get; set; }

        [JsonProperty("billingAddress")]
        public Address BillingAddress { get; set; }

        [JsonProperty("deliveryAddress")]
        public Address DeliveryAddress { get; set; }

        [JsonProperty("expiresAt")]
        public string ExpiresAt { get; set; }
    }

    public class Address
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("houseNumberOrName")]
        public string HouseNumberOrName { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("stateOrProvince")]
        public string StateOrProvince { get; set; }
    }
}