using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateways.PayPal
{
    public class PayPalAuthorizeRequest
    {
        public string paymentID { get; set; }
        public string payerID { get; set; }
    }
}
