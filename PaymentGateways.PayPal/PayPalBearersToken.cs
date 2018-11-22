using System;

namespace PaymentGateways.PayPal
{

    /// <summary>
    /// This is meant to be saved in your database and accesed only as a singleton, deleting and updateing when neccessary
    /// </summary>
    public class PayPalBearersToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime DateTimeObtained { get; set; }

        public string GetTokenForHeader()
        {
            string valueToReturn = "";
            if (!String.IsNullOrWhiteSpace(Token))
            {
                try
                {
                    PayPalToken payPalToken = Newtonsoft.Json.JsonConvert.DeserializeObject<PayPalToken>(Token);
                    valueToReturn += $"{payPalToken.Token_Type} {payPalToken.Access_Token}";
                }
                catch (Exception)
                {
                }
            }

            return valueToReturn;
        }
    }
}
