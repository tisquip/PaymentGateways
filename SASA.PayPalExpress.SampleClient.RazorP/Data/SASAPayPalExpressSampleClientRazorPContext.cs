using Microsoft.EntityFrameworkCore;
using PaymentGateways.PayPal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SASA.PayPalExpress.SampleClient.RazorP.Models
{
    public class SASAPayPalExpressSampleClientRazorPContext : DbContext
    {
        public SASAPayPalExpressSampleClientRazorPContext(DbContextOptions<SASAPayPalExpressSampleClientRazorPContext> options)
            : base(options)
        {
        }

        public DbSet<PayPalBearersToken> PayPalBearersToken { get; set; }

        public ISASAShoppingCart ShoppingCart(int shoppingCartId)
        {
            return new ShoppingCart
            {
                Description = "This is a purchase made on the SASA Paypal Client Example",
                InvoiceNumber = Guid.NewGuid().ToString(),
                CartItems = new List<ISASACartItem>()
                {
                    new CartItem()
                    {
                        Description = "Washing Towel made from the finest silk",
                        Name = "Silk Towel",
                        PricePerUnit = 3.09M,
                        Quantity = 3,
                        TotalPrice = (decimal)(3.09 * 3)
                    },
                     new CartItem()
                    {
                        Description = "Whole wheat slow baked",
                        Name = "Bread",
                        PricePerUnit = 1,
                        Quantity = 4,
                        TotalPrice = (4 * 1)
                    },
                      new CartItem()
                    {
                        Description = "Milk that is so super smooth",
                        Name = "Milk",
                        PricePerUnit = 7.88M,
                        Quantity = 6,
                        TotalPrice = (decimal)(7.88 * 6)
                    }
                },
                GrandTotalPrice = (decimal)(3.09 * 3) + (4 * 1) + (decimal)(7.88 * 6)
            };
        }

        public async Task UpdateShoppingCartToPaidStatus(int shoppingCartId, string payPalId)
        {
            //Update here
        }

        public string ReturnUrl(int userId)
        {
            return "http://localhost:53436/about";
        }

        public string CancelUrl()
        {
            return "http://localhost:53436/";
        }
    }
}
