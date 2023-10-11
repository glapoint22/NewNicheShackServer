using Website.Application.Common.Classes;

namespace Website.Application.ProductOrders.GetOrders.Classes
{
    public sealed class OrderProductDto
    {
        public Guid ProductId { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Image Image { get; set; } = null!;
        public string Hoplink { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public bool Disabled { get; set; }
    }
}