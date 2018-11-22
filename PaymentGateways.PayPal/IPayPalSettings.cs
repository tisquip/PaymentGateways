using System.Threading.Tasks;

namespace PaymentGateways.PayPal
{
    /// <summary>
    /// Do not implement directly, instead inherit from PayPalSettings Abstract class
    /// </summary>
    public interface IPayPalSettings
    {
        string LiveKey();
        string LiveClientId();
        string SandboxKey();
        string SandboxClientId();

        PayPalMode PayPalMode();
        Task<string> GetBearersToken(bool isExpired = false);
    }
}
