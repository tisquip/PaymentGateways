using Microsoft.AspNetCore.Mvc;
using PaymentGateways.PayPal;
using SASA.PayPalExpress.SampleClient.RazorP.Models;
using System;
using System.Threading.Tasks;

namespace SASA.PayPalExpress.SampleClient.RazorP.Controllers
{
    [Produces("application/json")]
    [Route("api/PayPal")]
    public class PayPalController : Controller
    {
        private readonly IPayPalSASA payPalSASA;
        private readonly SASAPayPalExpressSampleClientRazorPContext db;

        public PayPalController(IPayPalSASA payPalSASA, SASAPayPalExpressSampleClientRazorPContext db)
        {
            this.payPalSASA = payPalSASA;
            this.db = db;
        }

        [HttpPost("createPayment/{userId}/{shoppingCartId}")]
        public async Task<IActionResult> CreatePayment(int userId, int shoppingCartId)
        {
            try
            {
                string dataToReturn = await payPalSASA.CreatePayment(db.ShoppingCart(shoppingCartId), db.ReturnUrl(userId), db.CancelUrl());
                if (!String.IsNullOrWhiteSpace(dataToReturn))
                {
                    return Json(dataToReturn);
                }
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
                throw;
            }

            return BadRequest();
        }


        [HttpPost("executePayment/{shoppingCartId}")]
        public async Task<IActionResult> ExecutePayment(PayPalAuthorizeRequest payPalAuthorizeRequest, int shoppingCartId)
        {
            //Local function that is used as a callback function in the payPalSASA.ExecutePayment method
            async Task OnSuccess(string payPalId)
            {
                await db.UpdateShoppingCartToPaidStatus(shoppingCartId, payPalId);
            }

            try
            {
                await payPalSASA.ExecutePayment(payPalAuthorizeRequest, OnSuccess);
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
                throw;
            }

            return NoContent();
        }


    }
}