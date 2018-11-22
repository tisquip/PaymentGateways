using PaymentsGateways.PayNow;
using SASA.PayNow.SampleClient.RazorP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SASA.PayNow.SampleClient.RazorP.Services
{
    public class ShoppingCart : IShoppingCart
    {
        public string Reference { get; }

        public Dictionary<string, (decimal price, int quantity)> Cart { get; } = new Dictionary<string, (decimal price, int quantity)>();

        public string AdditionalInformation { get; }

        public decimal Total { get; }

        public ShoppingCart(ApplicationDbContext applicationDbContext)
        {

            PayNowObject payNowObject = applicationDbContext.PayNowObject.LastOrDefault(p => String.IsNullOrWhiteSpace(p.PollUrl));

            if (payNowObject == null)
            {
                var result = GetCartItems();
                payNowObject = new PayNowObject()
                {
                    PaymentStatus = PaymentStatus.Unknown,
                    Reference = Guid.NewGuid().ToString(),
                    Total = result.total,
                    JSDictionay = Newtonsoft.Json.JsonConvert.SerializeObject(result.cartItems)
                };

                applicationDbContext.PayNowObject.Add(payNowObject);
                applicationDbContext.SaveChanges();
            }

            Reference = payNowObject.Reference;
            Cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, (decimal price, int amount)>>(payNowObject.JSDictionay);

            Total = payNowObject.Total;

            AdditionalInformation = "Purchases made on the Sample Site, including hats, shoes etc";
        }

        private (decimal total, Dictionary<string, (decimal price, int quantity)> cartItems) GetCartItems()
        {
            Dictionary<string, decimal> CartSelection = new Dictionary<string, decimal>
                                                        {
                                                            { "Hat", 4.50M },
                                                            { "Pants", 3.20M },
                                                            { "Tie", 1.05M },
                                                            { "Watch", 35.50M },
                                                            { "Belt", 7.90M },
                                                            { "Sweater", 23.88M },
                                                            { "Shirt", 14.50M },
                                                            { "Sneakers", 42.50M },
                                                            { "Shoes", 10.99M },
                                                            { "Socks", 4.50M }
                                                        };

            Random rand = new Random();

            Dictionary<string, (decimal price, int quantity)> cartItems = new Dictionary<string, (decimal price, int quantity)>();

            List<int> selectedIds = new List<int>();
            decimal total = 0;

            for (int i = 0; i < 8; i++)
            {
                if (!selectedIds.Contains(i))
                {
                    int quantity = rand.Next(1, 9);
                    cartItems.Add(CartSelection.ElementAt(i).Key, (CartSelection.ElementAt(i).Value, quantity));
                    total += CartSelection.ElementAt(i).Value * quantity;
                }
            }
            return (total, cartItems);
        }
    }
}
