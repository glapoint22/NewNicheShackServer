namespace Website.Application.ProductOrders.PostOrder.Classes
{
    public sealed class OrderNotification
    {
        public string TransactionTime { get; set; } = string.Empty;
        public string Receipt { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public string Vendor { get; set; } = string.Empty;
        public string Affiliate { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public double TotalAccountAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public double TotalOrderAmount { get; set; }
        public IEnumerable<string> TrackingCodes { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<LineItem> LineItems { get; set; } = Enumerable.Empty<LineItem>();
        public CustomerShipping Customer { get; set; } = null!;
        public Upsell Upsell { get; set; } = null!;
        public float Version { get; set; }
        public int AttemptCount { get; set; }
    }
}