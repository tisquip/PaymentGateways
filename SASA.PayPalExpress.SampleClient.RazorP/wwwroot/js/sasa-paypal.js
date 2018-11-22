

var payPalEnvUnSanitized = document.getElementById("payPalMode").value;

var payPalEnv = "";
if (payPalEnvUnSanitized == "0") {
    payPalEnv = "sandbox";
} else {
    payPalEnv = "production";
}

var createPaymentUrl = document.getElementById("createPaymentUrl").value;
var executePaymentUrl = document.getElementById("executePaymentUrl").value;

paypal.Button.render({
    env: payPalEnv, // Or 'production' 'sandbox',

    commit: true, // Show a 'Pay Now' button

    style: {
        color: 'gold',
        size: 'small'
    },

    payment: function (data, actions) {
        return paypal.request.post(createPaymentUrl).then(function (data) {
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
        return paypal.request.post(executePaymentUrl, data)
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
        /*
         * Buyer cancelled the payment
         */
        console.log('cancelled');
    },

    onError: function (err) {
        /*
         * An error occurred during the transaction
         */

        console.log('error');
        console.log(err);
    }
}, '#paypal-button');

