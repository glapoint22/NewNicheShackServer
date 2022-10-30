namespace Shared.Common.Entities
{
    public sealed class ProductOrder
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public int PaymentMethod { get; set; }
        public double Subtotal { get; set; }
        public double ShippingHandling { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }

        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public ICollection<OrderProduct> OrderProducts { get; private set; } = new HashSet<OrderProduct>();
    }
}