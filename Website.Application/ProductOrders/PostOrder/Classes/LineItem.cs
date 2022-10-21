namespace Website.Application.ProductOrders.PostOrder.Classes
{
    public sealed class LineItem
    {
        public string ItemNo { get; set; } = string.Empty;
        public string ProductTitle { get; set; } = string.Empty;
        public bool Shippable { get; set; }
        public bool Recurring { get; set; }
        public double AccountAmount { get; set; }
        public int Quantity { get; set; }
        public PaymentPlan PaymentPlan { get; set; } = null!;
        public string LineItemType { get; set; } = string.Empty;
        public double ProductPrice { get; set; }
        public double ProductDiscount { get; set; }
        public double TaxAmount { get; set; }
        public double ShippingAmount { get; set; }
        public bool ShippingLiable { get; set; }
    }
}