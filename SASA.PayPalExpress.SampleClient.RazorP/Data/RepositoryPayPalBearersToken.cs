using Microsoft.EntityFrameworkCore;
using PaymentGateways.PayPal;
using SASA.PayPalExpress.SampleClient.RazorP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SASA.PayPalExpress.SampleClient.RazorP.Data
{
    public class RepositoryPayPalBearersToken : IRepositoryPayPalBearersToken
    {
        private readonly SASAPayPalExpressSampleClientRazorPContext db;

        public RepositoryPayPalBearersToken(SASAPayPalExpressSampleClientRazorPContext db)
        {
            this.db = db;
        }

        public async Task AddAndSaveNewPayPalBearersToken(PayPalBearersToken payPalBearersTokenToSave)
        {
            db.PayPalBearersToken.Add(payPalBearersTokenToSave);
            await db.SaveChangesAsync();
        }

        public async Task<PayPalBearersToken> GetFirstOrDefualtPayPalBearersTokenFromDatabase()
        {
            return await db.PayPalBearersToken.FirstOrDefaultAsync();
        }

        public async Task RemoveAllPayPalBearersTokensInDatatbase()
        {
            List<PayPalBearersToken> tokens = db.PayPalBearersToken.ToList();
            if (tokens.Count > 0)
            {
                db.PayPalBearersToken.RemoveRange(tokens);
                await db.SaveChangesAsync();
            }
        }
    }
}
