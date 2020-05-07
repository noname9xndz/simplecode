using AdyenPayment.Adyen.Models;
using AdyenPayment.Adyen.Models.Requests;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace AdyenPayment.Adyen.Requests
{
    public static class AdyenRequests
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="chargePaymentRequest"></param>
        /// <returns></returns>
        public static PaymentPostPaidResponse ChargePayment(ChargePaymentRequest chargePaymentRequest)
        {
            var responsePay = new PaymentPostPaidResponse();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(chargePaymentRequest.AdyenApiUrl);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Headers.Add("X-API-Key", chargePaymentRequest.AdyenApiKey);
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var data = new
                    {
                        merchantAccount = chargePaymentRequest.MerchantAccount,
                        amount = new
                        {
                            currency = chargePaymentRequest.Amount.Currency,
                            value = Helpers.Helpers.ConvertCurrentcyAdyen(chargePaymentRequest.Amount.Value.ToString(), chargePaymentRequest.Amount.Currency)
                        },
                        ////add attribute test error case
                        //additionalData = new
                        //{
                        //    RequestedTestAcquirerResponseCode = 6
                        //},
                        shopperReference = chargePaymentRequest.ShopperName,
                        paymentMethod = new
                        {
                            type = "scheme",
                            storedPaymentMethodId = chargePaymentRequest.StoredPaymentMethodId
                        },
                        reference = chargePaymentRequest.ClientId.ToUpper() + "-" + chargePaymentRequest.Reference + "-" + DateTime.Now.ToUniversalTime(),
                        returnUrl = "",
                        shopperInteraction = "ContAuth",
                        recurringProcessingModel = "Subscription"
                    };
                    string json = JsonConvert.SerializeObject(data);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var response = streamReader.ReadToEnd();
                    PaymentSuccess json = JsonConvert.DeserializeObject<PaymentSuccess>(response);
                    responsePay.payment = json;
                    return responsePay;
                }
            }
            catch (WebException e)
            {
                //todo logging
                //                if (e.Response == null)
                //                {
                //                    Logger<>.Error("Exception: " + e);
                //                    throw new HttpClientException((int) HttpStatusCode.RequestTimeout, "HTTP Exception timeout", null,
                //                        "No response", e);
                //                }
                //
                //                Logger.Error("Exception: " + e);
                var response = (HttpWebResponse)e.Response;
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var bodyResponse = sr.ReadToEnd();
                    PaymentError json = JsonConvert.DeserializeObject<PaymentError>(bodyResponse);
                    responsePay.paymentError = json;
                    return responsePay;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="refundPaymentRequest"></param>
        /// <returns></returns>
        public static PaymentPostPaidResponse RefundPayment(RefundPaymentRequest refundPaymentRequest)
        {
            var responsePay = new PaymentPostPaidResponse();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(refundPaymentRequest.AdyenApiUrl);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Headers.Add("X-API-Key", refundPaymentRequest.AdyenApiKey);
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var data = new
                    {
                        merchantAccount = refundPaymentRequest.MerchantAccount,
                        modificationAmount = new
                        {
                            currency = refundPaymentRequest.Amount.Currency,
                            value = Helpers.Helpers.ConvertCurrentcyAdyen(refundPaymentRequest.Amount.Value.ToString(), refundPaymentRequest.Amount.Currency)
                        },
                        originalReference = refundPaymentRequest.PspReference,
                        reference = refundPaymentRequest.ClientId.ToUpper() + "-" + refundPaymentRequest.Reference + "-" + DateTime.Now.ToUniversalTime(),
                    };
                    string json = JsonConvert.SerializeObject(data);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var response = streamReader.ReadToEnd();
                    PaymentSuccess json = JsonConvert.DeserializeObject<PaymentSuccess>(response);
                    responsePay.payment = json;
                    return responsePay;
                }
            }
            catch (WebException e)
            {
                //                if (e.Response == null)
                //                {
                //                    throw new HttpClientException((int)HttpStatusCode.RequestTimeout, "HTTP Exception timeout", null, "No response", e);
                //                }
                var response = (HttpWebResponse)e.Response;
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var bodyResponse = sr.ReadToEnd();
                    PaymentError json = JsonConvert.DeserializeObject<PaymentError>(bodyResponse);
                    responsePay.paymentError = json;
                    return responsePay;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chargeRequest"></param>
        /// <returns></returns>
        public static PaymentPostPaidResponse Charge(ChargeRequest chargeRequest)
        {
            var responsePay = new PaymentPostPaidResponse();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(chargeRequest.AdyenApiUrl);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Headers.Add("X-API-Key", chargeRequest.AdyenApiKey);
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var data = new
                    {
                        merchantAccount = chargeRequest.MerchantAccount,
                        amount = new
                        {
                            currency = chargeRequest.Amount.Currency,
                            value = Helpers.Helpers.ConvertCurrentcyAdyen(chargeRequest.Amount.Value.ToString(), chargeRequest.Amount.Currency)
                        },
                        shopperReference = chargeRequest.ShopperName,
                        paymentMethod = new
                        {
                            type = "scheme",
                            encryptedCardNumber = chargeRequest.Cardnumber,
                            encryptedExpiryMonth = chargeRequest.Month,
                            encryptedExpiryYear = chargeRequest.Year,
                            encryptedSecurityCode = chargeRequest.SercurityCode,
                            holderName = chargeRequest.Holdername,
                            storeDetails = true,
                        },
                        reference = chargeRequest.ClientId.ToUpper() + "-" + chargeRequest.Reference + "-" + DateTime.Now.ToUniversalTime(),
                        returnUrl = "",
                    };
                    string json = JsonConvert.SerializeObject(data);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    PaymentSuccess json = JsonConvert.DeserializeObject<PaymentSuccess>(streamReader.ReadToEnd());
                    responsePay.payment = json;
                }
                return responsePay;
            }
            catch (WebException e)
            {
                //                if (e.Response == null)
                //                {
                //                    throw new HttpClientException((int)HttpStatusCode.RequestTimeout, "HTTP Exception timeout", null, "No response", e);
                //                }
                var response = (HttpWebResponse)e.Response;
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var bodyResponse = sr.ReadToEnd();
                    PaymentError json = JsonConvert.DeserializeObject<PaymentError>(bodyResponse);
                    responsePay.paymentError = json;
                    return responsePay;
                }
            }
        }

        /// <summary>
        /// create token
        /// </summary>
        /// <param name="createTokenRequest"></param>
        /// <returns></returns>
        ///
        public static PaymentTokenResponse CreateToken(CreateTokenRequest createTokenRequest)
        {
            var responsePay = new PaymentTokenResponse();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(createTokenRequest.AdyenApiUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Headers.Add("X-API-Key", createTokenRequest.AdyenApiKey);
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var data = new
                {
                    merchantAccount = createTokenRequest.MerchantAccount,
                    amount = new
                    {
                        currency = createTokenRequest.Amount.Currency,
                        value = 0 // using when postpaid method
                    },
                    shopperReference = createTokenRequest.ShopperName,
                    paymentMethod = new
                    {
                        type = "scheme",
                        encryptedCardNumber = createTokenRequest.CardNumber,
                        encryptedExpiryMonth = createTokenRequest.ExpiryMonth,
                        encryptedExpiryYear = createTokenRequest.ExpiryYear,
                        encryptedSecurityCode = createTokenRequest.SecurityCode,
                        holderName = createTokenRequest.HolderName,
                        storeDetails = true,
                    },
                    reference = createTokenRequest.ClientID.ToUpper() + "-" + createTokenRequest.Reference + "-" + DateTime.Now.ToUniversalTime(),
                    returnUrl = "",
                };
                string json = JsonConvert.SerializeObject(data);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                PaymentToken json = JsonConvert.DeserializeObject<PaymentToken>(streamReader.ReadToEnd());
                responsePay.PaymentToken = json;
                responsePay.IsSuccess = true;
            }
            return responsePay;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="paymentDetailRequest"></param>
        /// <returns></returns>
        public static PaymentDetailsResponse GetPaymentDetails(PaymentDetailRequests paymentDetailRequest)
        {
            var responseDetail = new PaymentDetailsResponse();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(paymentDetailRequest.AdyenApiUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Headers.Add("X-API-Key", paymentDetailRequest.AdyenApiKey);
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var data = new
                {
                    merchantAccount = paymentDetailRequest.MerchantAccount,
                    amount = new
                    {
                        currency = paymentDetailRequest.Amount.Currency,
                        value = paymentDetailRequest.Amount.Value
                    },
                    shopperReference = paymentDetailRequest.ShopperName,
                    channel = "web"
                };
                string json = JsonConvert.SerializeObject(data);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseDetail = JsonConvert.DeserializeObject<PaymentDetailsResponse>(streamReader.ReadToEnd());
            }
            return responseDetail;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="generatePaymentLinkRequest"></param>
        /// <returns></returns>
        public static PayByLinkResponse GeneratePaymentLink(GeneratePaymentLinkRequest generatePaymentLinkRequest)
        {
            var responsePayLink = new PayByLinkResponse();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(generatePaymentLinkRequest.AdyenApiUrl);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Headers.Add("X-API-Key", generatePaymentLinkRequest.AdyenApiKey);
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var data = new
                    {
                        reference = generatePaymentLinkRequest.Reference,
                        amount = new
                        {
                            currency = generatePaymentLinkRequest.Amount.Currency,
                            value = Helpers.Helpers.ConvertCurrentcyAdyen(generatePaymentLinkRequest.Amount.Value.ToString(), generatePaymentLinkRequest.Amount.Currency)
                        },
                        description = generatePaymentLinkRequest.Description,
                        countryCode = generatePaymentLinkRequest.CountryCode,
                        merchantAccount = generatePaymentLinkRequest.MerchantAccount,
                        expiresAt = generatePaymentLinkRequest.ExpiresAt,
                        shopperReference = generatePaymentLinkRequest.ShopperReference ?? "",
                        shopperLocale = generatePaymentLinkRequest.ShopperLocale ?? "",
                        //shopperEmail = generatePaymentLinkRequest.shopperEmail,
                        //billingAddress = new
                        //{
                        //    street = generatePaymentLinkRequest.billingAddress.street,
                        //    postalCode = generatePaymentLinkRequest.billingAddress.postalCode,
                        //    city = generatePaymentLinkRequest.billingAddress.city,
                        //    houseNumberOrName = generatePaymentLinkRequest.billingAddress.houseNumberOrName,
                        //    country = generatePaymentLinkRequest.billingAddress.country,
                        //    stateOrProvince = generatePaymentLinkRequest.billingAddress.stateOrProvince
                        //},
                        //deliveryAddress = new
                        //{
                        //    street = generatePaymentLinkRequest.deliveryAddress.street,
                        //    postalCode = generatePaymentLinkRequest.deliveryAddress.postalCode,
                        //    city = generatePaymentLinkRequest.deliveryAddress.city,
                        //    houseNumberOrName = generatePaymentLinkRequest.deliveryAddress.houseNumberOrName,
                        //    country = generatePaymentLinkRequest.deliveryAddress.country,
                        //    stateOrProvince = generatePaymentLinkRequest.deliveryAddress.stateOrProvince
                        //},
                    };
                    string json = JsonConvert.SerializeObject(data);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var response = streamReader.ReadToEnd();
                    PayByLinkSuccess json = JsonConvert.DeserializeObject<PayByLinkSuccess>(response);
                    responsePayLink.PayByLink = json;
                    return responsePayLink;
                }
            }
            catch (WebException e)
            {
                //                if (e.Response == null)
                //                {
                //                    Logger.Error("Exception: " + e);
                //                    throw new HttpClientException((int)HttpStatusCode.RequestTimeout, "HTTP Exception timeout", null, "No response", e);
                //                }
                //                Logger.Error("Exception: " + e);
                var response = (HttpWebResponse)e.Response;
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var bodyResponse = sr.ReadToEnd();
                    PayByLinkError json = JsonConvert.DeserializeObject<PayByLinkError>(bodyResponse);
                    responsePayLink.PayByLinkError = json;
                    return responsePayLink;
                }
            }
        }
    }
}