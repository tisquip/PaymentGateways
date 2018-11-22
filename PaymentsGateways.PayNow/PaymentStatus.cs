using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentsGateways.PayNow
{
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Cancelled,
        Underpaid,
        AwaitingDelivery,
        Delivered,
        Sent,
        Disputed,
        Unknown
    }
}
