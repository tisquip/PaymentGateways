using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsGateways.PayNow;
using SASA.PayNow.SampleClient.RazorP.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SASA.PayNow.SampleClient.RazorP.Pages
{
    public class IndexModel : PageModel
    {
        public IShoppingCart ShoppingCart { get; }

        private string baseUrl = "https://localhost:44365";

        public IndexModel(IShoppingCart shoppingCart)
        {
            ShoppingCart = shoppingCart;
        }



        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {

                HttpResponseMessage responseMessage = await httpClient.GetAsync($"{baseUrl}/api/PayNow");

                if (responseMessage.IsSuccessStatusCode)
                {
                    try
                    {
                        PayNowInitiateResponseObject payNowInitiateResponseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<PayNowInitiateResponseObject>(await responseMessage.Content.ReadAsStringAsync());

                        return Redirect(payNowInitiateResponseObject.browserurl);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return RedirectToPage();
        }


    }
}
