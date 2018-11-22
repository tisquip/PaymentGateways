using PaymentsGateways.PayNow;
using System;

namespace SASA.PayNow.SampleClient.RazorP.Models
{
    public class PayNowObject
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string PollUrl { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Decimal Total { get; set; }
        public string JSDictionay { get; set; }
    }
}
