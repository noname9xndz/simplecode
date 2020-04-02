using AdyenPayment.Adyen.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdyenPayment.Adyen.Helpers
{
    public static class Helpers
    {
        public static decimal ConvertCurrentcyAdyen(string money, string currency)
        {
            try
            {
                CurrencyResponse response = new CurrencyResponse();
                string stateDataPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("/Adyen/Files/currency.json", StringComparison.Ordinal));
                if (System.IO.File.Exists(stateDataPath))
                {
                    var stateInfo = System.IO.File.ReadAllText(stateDataPath);
                    var items = JsonConvert.DeserializeObject<List<CurrencyResponse>>(stateInfo);
                    if (items != null && items != null)
                    {
                        if (currency != null)
                        {
                            response = items.Where(x => x.Code == currency).FirstOrDefault();
                        }
                        else
                        {
                            response = items.FirstOrDefault();
                        }
                    }
                    if (response.Point == "2")
                    {
                        return decimal.Parse(money) * 100;
                    }
                    if (response.Point == "3")
                    {
                        return decimal.Parse(money) * 1000;
                    }
                    if (response.Point == "0")
                    {
                        return decimal.Parse(money);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return decimal.Parse(money);
        }
    }
}