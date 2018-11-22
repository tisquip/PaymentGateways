using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentsGateways.PayNow
{
    public class PayNowPollResultObject
    {
        public string reference { get; set; }
        public string paynowreference { get; set; }
        public string amount { get; set; }
        public string status { get; set; }
        public string pollurl { get; set; }
        public string hash { get; set; }
        public string actulResponseStringRaw { get; set; }

        public PayNowPollResultObject()
        {
        }

        public PayNowPollResultObject(string responseString)
        {
            DecodeResponseString(responseString);
        }

        private void DecodeResponseString(string responseString)
        {
            actulResponseStringRaw = responseString;

            var keyValueRaw = HttpUtility.ParseQueryString(responseString);
            if (keyValueRaw != null && keyValueRaw.HasKeys())
            {
                List<string> keysRaw = keyValueRaw.AllKeys.ToList();
                if (keysRaw.Contains("reference"))
                {
                    reference = keyValueRaw["reference"];
                }

                if (keysRaw.Contains("paynowreference"))
                {
                    paynowreference = keyValueRaw["paynowreference"];
                }

                if (keysRaw.Contains("amount"))
                {
                    amount = keyValueRaw["amount"];
                }

                if (keysRaw.Contains("status"))
                {
                    status = keyValueRaw["status"];
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

        public string ConcatinatedValues()
        {
            string valueToReturn = "";

            valueToReturn += reference ?? "";
            valueToReturn += paynowreference?.UrlDecode() ?? "";
            valueToReturn += amount?.UrlDecode() ?? "";
            valueToReturn += status?.UrlDecode() ?? "";
            valueToReturn += pollurl?.UrlDecode() ?? "";

            return valueToReturn;
        }
    }
}
