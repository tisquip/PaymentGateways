using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsGateways.PayNow
{
    public class PayNowSASA : IPayNowSASA
    {
        private Uri payNowInitiatePostUri = new Uri("https://www.paynow.co.zw/interface/initiatetransaction");

        public async Task<(bool result, PayNowInitiateResponseObject payNowInitiateResponseObject)> InitiateOrDefualtAsync(int intergrationId, string intergrationKey, decimal totalAmount, string additionalInformation, string internalReference, string returnUrl, string resultUrl)
        {
            decimal amount = totalAmount;

            PayNowInitiateRequestObject payNowInitiateRequestObject = new PayNowInitiateRequestObject
            {
                id = intergrationId,
                additionalinfo = additionalInformation,
                amount = amount,
                reference = internalReference,
                returnurl = returnUrl,
                resulturl = resultUrl,
                status = "Message"
            };

            payNowInitiateRequestObject.hash = GenarateHash(payNowInitiateRequestObject.ConcatinatedValues(intergrationKey));

            string urlEncodedString = payNowInitiateRequestObject.UrlEncode();

            using (HttpClient httpClient = new HttpClient())
            {
                var dict = new Dictionary<string, string>();
                dict = payNowInitiateRequestObject.DictionaryKeyValues();
                var req = new HttpRequestMessage(HttpMethod.Post, payNowInitiatePostUri) { Content = new FormUrlEncodedContent(dict) };
                var result = await httpClient.SendAsync(req);


                if (result.IsSuccessStatusCode)
                {
                    string resultString = await result.Content.ReadAsStringAsync();
                    PayNowInitiateResponseObject responseObjectPayNow = new PayNowInitiateResponseObject(resultString);

                    if (HashIsVerified(responseObjectPayNow.ConcatinatedValues(), responseObjectPayNow.hash, intergrationKey))
                    {
                        if (responseObjectPayNow.status.ToUpper() == "OK")
                        {
                            return (true, responseObjectPayNow);
                        }
                    }
                }
            }
            return (false, null);
        }


        public async Task<(bool result, PayNowPollResultObject payNowPollResultObject)> PollAsyncOrDefualt(string pollUrl, string intergrationId)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsync(pollUrl, null);
                if (result.IsSuccessStatusCode)
                {
                    string responseString = await result.Content.ReadAsStringAsync();
                    PayNowPollResultObject payNowPollResultObject = new PayNowPollResultObject(responseString);
                    if (payNowPollResultObject != null && !String.IsNullOrWhiteSpace(payNowPollResultObject.hash) && HashIsVerified(payNowPollResultObject.ConcatinatedValues(), payNowPollResultObject.hash, intergrationId))
                    {
                        return (true, payNowPollResultObject);
                    }
                }
            }
            return (false, null);
        }

        private bool HashIsVerified(string concutinatedValues, string hash, string intergrationKey)
        {
            string concutinatedString = concutinatedValues + intergrationKey;

            string hashOfConcatinatedString = GenarateHash(concutinatedString);

            return hashOfConcatinatedString == hash;
        }


        private string GenarateHash(string concatValues)
        {

            string valueToReturn = "";

            var data = Encoding.UTF8.GetBytes(concatValues);
            using (SHA512 shaM = new SHA512Managed())
            {
                var hash = shaM.ComputeHash(data);

                StringBuilder hex = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                    hex.AppendFormat("{0:x2}", b);

                valueToReturn = hex.ToString().ToUpper();
            }

            return valueToReturn;
        }
    }
}
