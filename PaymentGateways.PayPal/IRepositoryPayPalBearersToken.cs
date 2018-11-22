using System.Threading.Tasks;

namespace PaymentGateways.PayPal
{
    public interface IRepositoryPayPalBearersToken
    {
        Task<PayPalBearersToken> GetFirstOrDefualtPayPalBearersTokenFromDatabase();

        Task RemoveAllPayPalBearersTokensInDatatbase();

        Task AddAndSaveNewPayPalBearersToken(PayPalBearersToken payPalBearersTokenToSave);
    }
}
