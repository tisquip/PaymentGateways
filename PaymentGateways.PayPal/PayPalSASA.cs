using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGateways.PayPal
{
    public class PayPalSASA : IPayPalSASA
    {
        private readonly IPayPalSettings payPalSettings;

        public PayPalSASA(IPayPalSettings payPalSettings)
        {
            this.payPalSettings = payPalSettings;
        }

        public async Task<string> CreatePayment(ISASAShoppingCart shoppingCart, string returnUrl, string cancelUrl = "")
        {
            if (String.IsNullOrWhiteSpace(cancelUrl))
            {
                cancelUrl = returnUrl;
            }

            RedirectUrls redirectUrls = new RedirectUrls()
            {
                cancel_url = cancelUrl,
                return_url = returnUrl
            };

            string valueToReturn = "";
            string bearersToken = await payPalSettings.GetBearersToken();
            if (!String.IsNullOrEmpty(bearersToken))
            {
                #region new
                bool isLiveMode = payPalSettings.PayPalMode() == PayPalMode.Live;

                var config = new Dictionary<string, string>
                {
                    { "mode", isLiveMode ? "live" : "sandbox" },
                    { "clientId", isLiveMode ? payPalSettings.LiveClientId () : payPalSettings.SandboxClientId() },
                    { "clientSecret", isLiveMode ? payPalSettings.LiveKey() : payPalSettings.SandboxKey() }
                };

                try
                {
                    var accessToken = new OAuthTokenCredential(config).GetAccessToken();
                    var apiContext = new APIContext(accessToken) { Config = config };

                    var payment = Payment.Create(apiContext, new Payment
                    {
                        intent = "sale",
                        payer = new Payer
                        {
                            payment_method = "paypal"
                        },
                        transactions = new List<Transaction>() { MapShoppingCart(shoppingCart) },
                        redirect_urls = redirectUrls
                    });

                    valueToReturn = Newtonsoft.Json.JsonConvert.SerializeObject(payment);
                }
                catch (Exception)
                {
                }

                #endregion new

            }
            return valueToReturn;
        }

        public async Task<(bool result, string executedPaymentId)> ExecutePayment(PayPalAuthorizeRequest payPalAuthorizeRequest, Func<string, Task> onSuccessCallBack)
        {
            #region new

            string paymentID = payPalAuthorizeRequest.paymentID;
            string payerId = payPalAuthorizeRequest.payerID;


            bool isLiveMode = payPalSettings.PayPalMode() == PayPalMode.Live;

            var config = new Dictionary<string, string>();
            config.Add("mode", isLiveMode ? "live" : "sandbox");
            config.Add("clientId", isLiveMode ? payPalSettings.LiveClientId() : payPalSettings.SandboxClientId());
            config.Add("clientSecret", isLiveMode ? payPalSettings.LiveKey() : payPalSettings.SandboxKey());


            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken) { Config = config };

            // Using the information from the redirect, setup the payment to execute.
            var paymentId = paymentID;
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };

            // Execute the payment.
            var executedPayment = payment.Execute(apiContext, paymentExecution);
            if (executedPayment != null && executedPayment.state == "approved")
            {
                await onSuccessCallBack(executedPayment.id);

                return (true, executedPayment.id);
            }
            #endregion new

            return (false, null);
        }

        private Item MapCreatItem(ISASACartItem cartItem)
        {
            return new Item()
            {
                name = cartItem.Name,
                currency = "USD",
                description = cartItem?.Description ?? "No Description Given",
                price = cartItem.PricePerUnit.ToString("0.00"),
                quantity = cartItem.Quantity.ToString()
            };
        }

        private List<Item> MapCartItem(List<ISASACartItem> cartItems)
        {
            List<Item> valueToReturn = new List<Item>();

            foreach (var cartItem in cartItems)
            {
                valueToReturn.Add(MapCreatItem(cartItem));
            }

            return valueToReturn;
        }

        private Transaction MapShoppingCart(ISASAShoppingCart shoppingCart)
        {
            return new Transaction()
            {
                description = shoppingCart?.Description ?? "No Description Given",
                invoice_number = shoppingCart.InvoiceNumber,
                amount = new Amount()
                {
                    currency = "USD",
                    total = shoppingCart.GrandTotalPrice.ToString("0.00")
                },
                item_list = new ItemList
                {
                    items = MapCartItem(shoppingCart.CartItems)
                }
            };

        }
    }
}
