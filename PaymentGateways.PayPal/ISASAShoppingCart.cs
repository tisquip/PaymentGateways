using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateways.PayPal
{
    public interface ISASAShoppingCart
    {
        List<ISASACartItem> CartItems { get; set; }
        string Description { get; set; }

        /// <summary>
        /// The total of everything in the cart
        /// </summary>
        decimal GrandTotalPrice { get; set; }

        /// <summary>
        /// Unique for every transaction. Can be a Guid.
        /// </summary>
        string InvoiceNumber { get; set; }
    }
}
