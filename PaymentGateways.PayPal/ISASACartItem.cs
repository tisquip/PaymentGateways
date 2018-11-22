using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateways.PayPal
{
    public interface ISASACartItem
    {
        string Description { get; set; }
        string Name { get; set; }
        decimal PricePerUnit { get; set; }
        int Quantity { get; set; }

        /// <summary>
        /// The full line total (PricePerUnit * Quntity)
        /// </summary>
        decimal TotalPrice { get; set; }
    }
}
