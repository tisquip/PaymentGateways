namespace PaymentGateways.PayPal
{
    /// <summary>
    /// In progress. Do not use, need to figure out how to load scripts from a .net method
    /// </summary>
    public interface IPayPalSASAScript
    {
        string Script(string createPaymentUrl, string executePaymentUrl, PayPalMode payPalMode);
    }
}
