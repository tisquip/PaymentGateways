using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateways.PayPal
{
    /// <summary>
    /// Inherit from PayPalSettings instead of implementing the IPayPalSettings interface. Also insure that PayPalBearersToken has one in the database only, there can be none, by which the this abstract class trys to get a token, then sends it to try and be saved
    /// </summary>
    public abstract class PayPalSettingsBaseClass : IPayPalSettings
    {
        private readonly IRepositoryPayPalBearersToken repositoryPayPalBearersToken;

        public abstract string LiveClientId();

        public abstract string LiveKey();

        public abstract PayPalMode PayPalMode();

        public abstract string SandboxClientId();

        public abstract string SandboxKey();



        public PayPalSettingsBaseClass(IRepositoryPayPalBearersToken repositoryPayPalBearersToken)
        {
            this.repositoryPayPalBearersToken = repositoryPayPalBearersToken;
        }

        public async Task<string> GetBearersToken(bool isExpired = false)
        {
            PayPalBearersToken payPalBearersToken = await repositoryPayPalBearersToken.GetFirstOrDefualtPayPalBearersTokenFromDatabase();
            if (isExpired && payPalBearersToken != null)
            {
                await repositoryPayPalBearersToken.RemoveAllPayPalBearersTokensInDatatbase();
                payPalBearersToken = null;
            }

            if (payPalBearersToken == null)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    bool isLive = PayPalMode() == PayPal.PayPalMode.Live;
                    string clientId = isLive ? LiveClientId() : SandboxClientId();
                    string clientSecret = isLive ? LiveKey() : SandboxKey();

                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(
                            "Basic",
                            Convert.ToBase64String(
                                System.Text.ASCIIEncoding.ASCII.GetBytes(
                                    string.Format("{0}:{1}", clientId, clientSecret))));


                    var content = new FormUrlEncodedContent(new[]
                                    {
                                         new KeyValuePair<string, string>("grant_type", "client_credentials")
                                    });

                    DateTime timeOfRequest = DateTime.Now;

                    var result = await httpClient.PostAsync(UriOAuth(), content);

                    if (result.IsSuccessStatusCode)
                    {
                        payPalBearersToken = new PayPalBearersToken
                        {
                            Token = await result.Content.ReadAsStringAsync(),
                            DateTimeObtained = timeOfRequest
                        };

                        await repositoryPayPalBearersToken.RemoveAllPayPalBearersTokensInDatatbase();

                        await repositoryPayPalBearersToken.AddAndSaveNewPayPalBearersToken(payPalBearersToken);
                    }
                }
            }
            return payPalBearersToken.GetTokenForHeader();
        }

        private Uri UriOAuth()
        {
            if (PayPalMode() == PayPal.PayPalMode.Live)
            {
                return new Uri("https://api.paypal.com/v1/oauth2/token");
            }
            else
            {
                return new Uri("https://api.sandbox.paypal.com/v1/oauth2/token");
            }
        }
    }
}
