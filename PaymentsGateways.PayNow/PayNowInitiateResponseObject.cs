using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentsGateways.PayNow
{
    public class PayNowInitiateResponseObject
    {
        /// <summary>
        /// The URL on Paynow that the merchant site will redirect the Customer’s browser to.Upon redirect the Customer will perform their transaction.
        /// </summary>
        public string browserurl { get; set; }

        /// <summary>
        /// The URL on Paynow the merchant site can poll to confirm the transaction’s current status.
        /// </summary>
        public string pollurl { get; set; }
        public string hash { get; set; }

        /// <summary>
        /// It should be set as ok by paynow if all is well to “Ok” at this stage of the transaction.
        /// </summary>
        public string status { get; set; }
        public string actulResponseStringRaw { get; set; }


        public PayNowInitiateResponseObject()
        {
        }

        public string ConcatinatedValues()
        {
            string valueToReturn = "";

            valueToReturn += status ?? "";
            valueToReturn += browserurl?.UrlDecode() ?? "";
            valueToReturn += pollurl?.UrlDecode() ?? "";

            return valueToReturn;
        }

        public PayNowInitiateResponseObject(string responseFromPayNow)
        {
            actulResponseStringRaw = responseFromPayNow;
            DecodeResponseString(actulResponseStringRaw);
        }

        private void DecodeResponseString(string responseString)
        {
            actulResponseStringRaw = responseString;

            var keyValueRaw = HttpUtility.ParseQueryString(responseString);
            if (keyValueRaw != null && keyValueRaw.HasKeys())
            {
                List<string> keysRaw = keyValueRaw.AllKeys.ToList();
                if (keysRaw.Contains("status"))
                {
                    status = keyValueRaw["status"];
                }

                if (keysRaw.Contains("browserurl"))
                {
                    browserurl = keyValueRaw["browserurl"];
                }

                if (keysRaw.Contains("pollurl"))
                {
                    pollurl = keyValueRaw["pollurl"];
                }

                if (keysRaw.Contains("hash"))
                {
                    hash = keyValueRaw["hash"];
                }
            }
        }

    }
}
