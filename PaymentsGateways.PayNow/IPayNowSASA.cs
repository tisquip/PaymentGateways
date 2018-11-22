using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsGateways.PayNow
{
    public interface IPayNowSASA
    {
        /// <summary>
        /// Initiates the payment request with the PayNow Server. check the returned payNowInitiateResponseObject.status.ToUpper() == "OK" and then set the status of your payment to PaymentStatus.Pending because you still have to poll the pay now server to check whether the status was actually ok and paid
        /// </summary>
        /// <param name="intergrationId">Your intergation Id from PayNow</param>
        /// <param name="intergrationKey">Your intergation Key from PayNow</param>
        /// <param name="totalAmount">The total amount to charge</param>
        /// <param name="additionalInformation">Your cart items description or the service the person is paying for</param>
        /// <param name="internalReference">Your unique reference of the invoice</param>
        /// <param name="returnUrl">The url paynow will redirect the user to</param>
        /// <param name="resultUrl">The url where paynow will make a backchanel post to update the transaction</param>
        /// <returns>A tuple, result is true or false depending on the success of the request, The payNowInitiateResponseObject can be null or can contains details of the transaction, for example, the status as well as the poll url that can be used to check the status of that particular payment</returns>
        Task<(bool result, PayNowInitiateResponseObject payNowInitiateResponseObject)> InitiateOrDefualtAsync(int intergrationId, string intergrationKey, decimal totalAmount, string additionalInformation, string internalReference, string returnUrl, string resultUrl);

        /// <summary>
        /// Used to poll the status of a payment on the PayNow Servers
        /// </summary>
        /// <param name="pollUrl">The url to poll given by PayNow after Initiation of the transaction</param>
        /// <param name="intergrationId">The PayNow Intergration Id from PayNow</param>
        /// <returns>A tuple, result if the request was a success or not,asn also a payNowPollResultObject which can be null or can contain things such as status</returns>
        Task<(bool result, PayNowPollResultObject payNowPollResultObject)> PollAsyncOrDefualt(string pollUrl, string intergrationId);
    }
}
