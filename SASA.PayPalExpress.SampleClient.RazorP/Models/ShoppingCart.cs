using PaymentGateways.PayPal;
using System.Collections.Generic;

namespace SASA.PayPalExpress.SampleClient.RazorP.Models
{
    public class ShoppingCart : ISASAShoppingCart
    {
        public List<ISASACartItem> CartItems { get; set; }
        public string Description { get; set; }
        public decimal GrandTotalPrice { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
