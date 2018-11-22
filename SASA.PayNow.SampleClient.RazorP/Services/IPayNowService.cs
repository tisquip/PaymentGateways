using PaymentsGateways.PayNow;
using System.Threading.Tasks;

namespace SASA.PayNow.SampleClient.RazorP.Services
{
    public interface IPayNowService
    {
        Task<(bool result, PayNowInitiateResponseObject payNowInitiateResponseObject)> InitiateOrDefualtAsync(decimal totalAmount, string additionalInformation, string internalReference, string returnUrl, string resultUrl);

        Task<(bool result, PayNowPollResultObject payNowPollResultObject)> PollAsyncOrDefualt(string pollUrl);

    }
}
