using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateways.PayPal
{
    public class PayPalSASAScript : IPayPalSASAScript
    {
        public string Script(string createPaymentUrl, string executePaymentUrl, PayPalMode payPalMode)
        {
            string payPalEnv = "sandbox";
            if (payPalMode == PayPalMode.Live)
            {
                payPalEnv = "production";
            }
            string script = @"

<script src='https://www.paypalobjects.com/api/checkout.js'></script>
    <script>
        paypal.Button.render({

            env: " + payPalEnv + @", 

            commit: true, 

            style: {
                color: 'gold',
                size: 'small'
            },

            payment: function (data, actions) {
                return paypal.request.post(" + createPaymentUrl + @").then(function (data) {
                    var data = JSON.parse(data);
                    return data.id;
                });
            },

            onAuthorize: function (data, actions) {

                // Set up the data you need to pass to your server
                var data = {
                    paymentID: data.paymentID,
                    payerID: data.payerID
                };

                // Make a call to your server to execute the payment
                return paypal.request.post(" + executePaymentUrl + @", data)
                    .then(function (res) {
                        // The payment is complete!
                        // You can now show a confirmation message to the customer
                        if (res == 'true' || res == true) {

                            //window.location.replace('dashboard/orders');
                            window.location.replace('/');
                        }
                    });
            },

            onCancel: function (data, actions) {
                console.log('cancelled');
            },
            onError: function (err) {
                console.log('error');
                console.log(err);
            }
        }, '#paypal-button');

</script>

";

            return script;
        }
    }
}
