using PaymentGateways.PayPal;

namespace SASA.PayPalExpress.SampleClient.RazorP.Models
{
    public class CartItem : ISASACartItem
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
