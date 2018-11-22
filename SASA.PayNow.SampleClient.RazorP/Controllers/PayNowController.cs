using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentsGateways.PayNow;
using SASA.PayNow.SampleClient.RazorP.Models;
using SASA.PayNow.SampleClient.RazorP.Services;
using System;
using System.Threading.Tasks;

namespace SASA.PayNow.SampleClient.RazorP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayNowController : ControllerBase
    {
        private string baseUrl = "https://localhost:44365";
        private readonly IPayNowService payNowService;
        private readonly IShoppingCart shoppingCart;
        private readonly ApplicationDbContext applicationDbContext;

        public PayNowController(IPayNowService payNowService, IShoppingCart shoppingCart, ApplicationDbContext applicationDbContext)
        {
            this.payNowService = payNowService;
            this.shoppingCart = shoppingCart;
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            (bool result, PayNowInitiateResponseObject payNowResponseRequest) = await payNowService.InitiateOrDefualtAsync(shoppingCart.Total, shoppingCart.AdditionalInformation, shoppingCart.Reference, $"{baseUrl}/invoice/{shoppingCart.Reference}", $"{baseUrl}/api/PayNow/result/{shoppingCart.Reference}");

            if (result && payNowResponseRequest != null)
            {
                PayNowObject payNowObject = await applicationDbContext.PayNowObject.SingleOrDefaultAsync(p => !String.IsNullOrWhiteSpace(p.Reference) && p.Reference == shoppingCart.Reference);

                if (payNowObject != null)
                {
                    payNowObject.PollUrl = payNowResponseRequest.pollurl;
                    payNowObject.PaymentStatus = PayNowStatus.GetPaymentStatus(payNowResponseRequest.status);
                    if (payNowObject.PaymentStatus == PaymentStatus.Paid)
                    {
                        payNowObject.PaymentStatus = PaymentStatus.Pending;
                    }

                    applicationDbContext.PayNowObject.Update(payNowObject);
                    await applicationDbContext.SaveChangesAsync();

                    return Ok(payNowResponseRequest);
                }
            }
            return BadRequest();
        }

        [HttpGet("poll/{reference}")]
        public async Task<IActionResult> poll(string reference)
        {
            await PollPayNowServer(reference);
            return NoContent();
        }



        [EnableCors("AllowAll")]
        [HttpPost("results/{reference}")]
        public async Task<IActionResult> results(string reference)
        {
            await PollPayNowServer(reference);
            return NoContent();
        }

        private async Task PollPayNowServer(string reference)
        {
            PayNowObject payNowObject = await applicationDbContext.PayNowObject.SingleOrDefaultAsync(p => !string.IsNullOrWhiteSpace(p.Reference) && p.Reference == reference && !String.IsNullOrWhiteSpace(p.PollUrl));

            if (payNowObject != null)
            {
                var results = await payNowService.PollAsyncOrDefualt(payNowObject.PollUrl);
                if (results.result && results.payNowPollResultObject != null)
                {
                    payNowObject.PaymentStatus = PayNowStatus.GetPaymentStatus(results.payNowPollResultObject.status);
                    applicationDbContext.PayNowObject.Update(payNowObject);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }
    }
}