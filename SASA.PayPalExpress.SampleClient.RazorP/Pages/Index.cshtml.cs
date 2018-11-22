using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentGateways.PayPal;
using SASA.PayPalExpress.SampleClient.RazorP.Models;

namespace SASA.PayPalExpress.SampleClient.RazorP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SASAPayPalExpressSampleClientRazorPContext db;
        public IPayPalSASAScript script { get; }
        ISASAShoppingCart ShoppingCart;

        public string CreatePaymentUrl { get; set; } = "/api/PayPal/createPayment/12/34";
        public string ExecutePaymentUrl { get; set; } = "/api/PayPal/executePayment/34";
        public int PayPalSelectedMode { get; set; } = (int)PayPalMode.Sandbox; //


        public IndexModel(SASAPayPalExpressSampleClientRazorPContext db, IPayPalSASAScript script)
        {
            this.db = db;
            this.script = script;
        }

        public void OnGet()
        {
            ShoppingCart = db.ShoppingCart(34);

        }

    }
}
