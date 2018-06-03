using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Paypal
{
    public class PaypalLogic
    {
        public static PaypalRedirect ExpressCheckout(PaypalOrder order)
        {

            Dictionary<string, string> values = new Dictionary<string, string>();

            values["USER"] = PaypalSettings.Username;
            values["PWD"] = PaypalSettings.Password;
            values["SIGNATURE"] = PaypalSettings.Signature;
            values["METHOD"] = "SetExpressCheckout";
            values["VERSION"] = "93";//2.3
            values["RETURNURL"] = PaypalSettings.ReturnUrl;
            values["CANCELURL"] = PaypalSettings.CancelUrl;
            values["AMT"] = order.Amount.ToString();
            values["TRXTYPE"] = "S";
            //values["PAYMENTACTION"] = "Sale";
            values["CURRENCYCODE"] = "USD";
            //values["BUTTONSOURCE"] = "PP-ECWizard";
            //values["SUBJECT"] = "Test";

            values = Submit(values);

            string ack = values["ACK"].ToLower();

            if (ack == "success" || ack == "successwithwarning")
            {
                return new PaypalRedirect
                {
                    Token = values["TOKEN"],
                    Url = String.Format("{0}?cmd=_express-checkout&token={1}",
                      PaypalSettings.CgiDomain, values["TOKEN"])
                };
            }
            else
            {
                throw new Exception(values["L_LONGMESSAGE0"]);
            }
        }

        private static Dictionary<string, string> Submit(Dictionary<string, string> values)
        {
            HttpContent content = new FormUrlEncodedContent(values);
            Task<HttpResponseMessage> response = new HttpClient().PostAsync(PaypalSettings.ApiDomain, content);

            string responceResult = response.Result.Content.ReadAsStringAsync().Result;
            Dictionary<string, StringValues> result = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(responceResult);
            Dictionary<string, string> finalResult = result.ToDictionary(item => item.Key, item => item.Value[0]);

            return finalResult;
        }

    }
}
