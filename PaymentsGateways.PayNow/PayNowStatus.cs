namespace PaymentsGateways.PayNow
{
    public static class PayNowStatus
    {
        public static string Paid { get; set; } = "Paid";
        public static string AwaitingDelivery { get; set; } = "Awaiting Delivery";
        public static string Delivered { get; set; } = "Delivered";
        public static string Created { get; set; } = "Created";
        public static string Sent { get; set; } = "Sent";
        public static string Cancelled { get; set; } = "Cancelled";
        public static string Disputed { get; set; } = "Disputed";
        public static string Refunded { get; set; } = "Refunded";

        public static PaymentStatus GetPaymentStatus(string payNowStatuses)
        {
            payNowStatuses = payNowStatuses.Trim().ToUpper();
            if (payNowStatuses == Paid.Trim().ToUpper())
            {
                return PaymentStatus.Paid;
            }
            else if (payNowStatuses == AwaitingDelivery.Trim().ToUpper())
            {
                return PaymentStatus.AwaitingDelivery;
            }
            else if (payNowStatuses == Delivered.Trim().ToUpper())
            {
                return PaymentStatus.Delivered;
            }
            else if (payNowStatuses == Sent.Trim().ToUpper())
            {
                return PaymentStatus.Sent;
            }
            else if (payNowStatuses == Cancelled.Trim().ToUpper())
            {
                return PaymentStatus.Cancelled;
            }
            else if (payNowStatuses == Disputed.Trim().ToUpper())
            {
                return PaymentStatus.Disputed;
            }

            return PaymentStatus.Unknown;
        }
    }
}
