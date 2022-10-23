using Website.Application.Common.Classes;

namespace Website.Application.ProductOrders.GetOrders.Classes
{
    public sealed class ProductOrderProduct
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Image Image { get; set; } = null!;
        public string RebillFrequency { get; set; } = string.Empty;
        public double RebillAmount { get; set; }
        public int PaymentsRemaining { get; set; }
        public string UrlName { get; set; } = string.Empty;
    }
}