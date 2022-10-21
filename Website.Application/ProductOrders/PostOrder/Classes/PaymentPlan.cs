namespace Website.Application.ProductOrders.PostOrder.Classes
{
    public sealed class PaymentPlan
    {
        public string RebillStatus { get; set; } = string.Empty;
        public string RebillFrequency { get; set; } = string.Empty;
        public double RebillAmount { get; set; }
        public int PaymentsProcessed { get; set; }
        public int PaymentsRemaining { get; set; }
        public string NextPaymentDate { get; set; } = string.Empty;
    }
}