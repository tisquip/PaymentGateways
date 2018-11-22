using Microsoft.Extensions.Configuration;
using PaymentsGateways.PayNow;
using System.Threading.Tasks;

namespace SASA.PayNow.SampleClient.RazorP.Services
{
    public class PayNowService : IPayNowService
    {
        private readonly IPayNowSASA payNowSASA;
        private readonly int intergrationId;
        private readonly string intergrationKey;


        public PayNowService(IPayNowSASA payNowSASA, IConfiguration configuration)
        {
            this.payNowSASA = payNowSASA;

            // These are gotten from the UserSecrets Manager, so check that instead of appsettings.json directly
            intergrationId = int.Parse(configuration.GetSection("PayNow").GetSection("IntergrationId").Value);
            intergrationKey = configuration.GetSection("PayNow").GetSection("IntergrationKey").Value;
        }

        public async Task<(bool result, PayNowInitiateResponseObject payNowInitiateResponseObject)> InitiateOrDefualtAsync(decimal totalAmount, string additionalInformation, string internalReference, string returnUrl, string resultUrl)
        {
            return await payNowSASA.InitiateOrDefualtAsync(intergrationId, intergrationKey, totalAmount, additionalInformation, internalReference, returnUrl, resultUrl);
        }

        public async Task<(bool result, PayNowPollResultObject payNowPollResultObject)> PollAsyncOrDefualt(string pollUrl)
        {
            return await payNowSASA.PollAsyncOrDefualt(pollUrl, intergrationKey);
        }
    }
}
