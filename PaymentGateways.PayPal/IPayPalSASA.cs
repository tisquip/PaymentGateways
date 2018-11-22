using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateways.PayPal
{
    public interface IPayPalSASA
    {
        /// <summary>
        /// This method is called by paypal to set up the transaction details, so the shopping cart.
        /// </summary>
        /// <param name="shoppingCart">The items including the decriptions and prices of the items</param>
        /// <param name="returnUrl">The url where the user is directed to when a succesful purchase has been completed. eg to /user/23/invoice/871 where the details of the invoice of that purchase are availabe</param>
        /// <param name="cancelUrl">The url the user is directed to if the user cancels the payment</param>
        /// <returns>A json serialized representation of the created payment from paypal</returns>
        Task<string> CreatePayment(ISASAShoppingCart shoppingCart, string returnUrl, string cancelUrl = "");

        /// <summary>
        /// This is called by PayPal after the success of the create payment
        /// </summary>
        /// <param name="payPalAuthorizeRequest">This is the object (data) returned from the javascript section when the execute payment is called</param>
        /// <param name="onSuccessCallBack">Process to take on success of payment. The string is an Id returned from paypal for reference purposes</param>
        /// <returns>Whether the operation was successful as well as the Id returned from paypal for reference purposes</returns>
        Task<(bool result, string executedPaymentId)> ExecutePayment(PayPalAuthorizeRequest payPalAuthorizeRequest, Func<string, Task> onSuccessCallBack);
    }
}
