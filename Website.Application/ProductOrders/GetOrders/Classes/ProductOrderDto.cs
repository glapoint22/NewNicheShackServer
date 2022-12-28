namespace Website.Application.ProductOrders.GetOrders.Classes
{
    public sealed class ProductOrderDto
    {
        public string OrderNumber { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentMethodImg { get; set; } = string.Empty;
        public double Subtotal { get; set; }
        public double ShippingHandling { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
        public Guid ProductId { get; set; }
        public string Hoplink { get; set; } = string.Empty;
        public List<ProductOrderProduct> Products { get; set; } = new List<ProductOrderProduct>();
    }
}