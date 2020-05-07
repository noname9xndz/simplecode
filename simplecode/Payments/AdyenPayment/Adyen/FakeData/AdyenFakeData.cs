namespace AdyenPayment.Adyen.FakeData
{
    public class AdyenFakeData
    {
        public string AdyenApiKeyFake => "AQEuhmfuXNWTK0Qc+iSRgXQxteG3QY1IHodTVGBF73aoH2pMsVQarkXQeDfxmx55SxDBXVsNvuR83LVYjEgiTGAH-zuR2BVdYot8CEt5e49WmbgPvN5/RufkudJssrGAlYdg=-Zsnt8h4YRa4JyGn7";
        public string AdyenOriginFake => "pub.v2.8015635625951579.aHR0cHM6Ly9wYXltZW50LXBvcnRhbC5sb2NhbA.R0FxFIRuf25bJNjetbllQv7DT8k263I-e9iu2OZV-pA";
        public string AdyenGeneratePaymentLink => "https://checkout-test.adyen.com/v51/paymentLinks";
        public string AdyenRefund => "https://pal-test.adyen.com/pal/servlet/Payment/v51/refund";
        public string AdyenPaymentMethods => "https://checkout-test.adyen.com/v50/paymentMethods";
        public string AdyenCheckout => "https://checkoutshopper-test.adyen.com/checkoutshopper/";
        public string AdyenPayment => "https://checkout-test.adyen.com/v49/payments";
        public string AdyenJs => "https://checkoutshopper-test.adyen.com/checkoutshopper/sdk/3.0.0/adyen.js";
        public string AdyenCss => "https://checkoutshopper-test.adyen.com/checkoutshopper/sdk/3.0.0/adyen.css";
        public string CurrencyFake => "US";
        public string MerchantAccount => "AspireHK";
    }
}