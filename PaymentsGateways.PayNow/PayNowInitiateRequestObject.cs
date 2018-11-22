using System;
using System.Collections.Generic;

namespace PaymentsGateways.PayNow
{
    public class PayNowInitiateRequestObject
    {
        /// <summary>
        /// Integration ID shown to the merchant in the “3rd Party Site or Link Profile” area of “Receive Payment Links” section of “Sell or Receive” on Paynow.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The transaction’s reference on the merchant site, this should be unique to the transaction.
        /// </summary>
        public string reference { get; set; }

        /// <summary>
        /// Final amount of the transaction, in USD to two decimal places (do not include a currency symbol).        /// </summary>
        public decimal amount { get; set; }

        /// <summary>
        /// (optional) Additional info to be displayed on Paynow to the Customer.This should not include any confidential information
        /// </summary>
        public string additionalinfo { get; set; }

        /// <summary>
        /// The URL on the merchant website the customer will be returned to after the transaction has been processed.It is recommended this URL contains enough information for the merchant site to identify the transaction.
        /// </summary>
        public string returnurl { get; set; }

        /// <summary>
        /// The URL on the merchant website Paynow will post transaction results to.It is recommended this URL contains enough information for the merchant site to identify the transaction
        /// </summary>
        public string resulturl { get; set; }



        /// <summary>
        /// (optional) If the field is present and set to an email address Paynow will attempt to auto login the customers email address as an anonymous user.If the email address has a registered account the user will be prompted to login to that account.
        /// </summary>
        public string authemail { get; set; }

        public string UrlEncode()
        {

            return $"resulturl={resulturl.UrlEncode()}&returnurl={returnurl.UrlEncode()}&reference={reference.UrlEncode()}&amount={amount}&id={id}&additionalinfo={(!String.IsNullOrWhiteSpace(additionalinfo) ? additionalinfo.UrlEncode() : "")}&authemail={(!String.IsNullOrWhiteSpace(authemail) ? authemail.UrlEncode() : "")}&status=Message&hash={hash}";

        }

        public Dictionary<string, string> DictionaryKeyValues()
        {
            Dictionary<string, string> valueToReturn = new Dictionary<string, string>();

            valueToReturn.Add("resulturl", resulturl);
            valueToReturn.Add("returnurl", returnurl);
            valueToReturn.Add("reference", reference);
            valueToReturn.Add("amount", amount.ToString());
            valueToReturn.Add("id", id.ToString());
            valueToReturn.Add("additionalinfo", ((!String.IsNullOrWhiteSpace(additionalinfo)) ? additionalinfo : ""));
            valueToReturn.Add("authemail", ((!String.IsNullOrWhiteSpace(authemail)) ? authemail : ""));
            valueToReturn.Add("status", status.UrlEncode());
            valueToReturn.Add("hash", hash);


            return valueToReturn;
        }

        /// <summary>
        /// Should be set to “Message” at this stage of the transaction.
        /// </summary>
        public string status { get; set; } = "Message";

        public string hash { get; set; }

        public string ConcatinatedValues(string merchantKey)
        {
            string valueToReturn =
                (resulturl?.Trim() ?? "") + (returnurl?.Trim() ?? "") + (reference?.Trim() ?? "") + (amount) + (id) + (additionalinfo?.Trim() ?? "") + (authemail?.Trim() ?? "") + (status?.Trim() ?? "") + merchantKey;


            return valueToReturn;
        }
    }
}
