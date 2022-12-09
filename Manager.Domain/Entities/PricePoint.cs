using Manager.Domain.Dtos;
using Shared.Common.ValueObjects;

namespace Manager.Domain.Entities
{
    public sealed class PricePoint
    {
        public Guid Id { get; set; }
        public Guid ProductPriceId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public Guid? ImageId { get; set; }
        public string? Header { get; set; }
        public string? Quantity { get; set; }
        public string? UnitPrice { get; set; }
        public string? Unit { get; set; }
        public string? StrikethroughPrice { get; set; }
        public int ShippingType { get; set; }
        public RecurringPayment RecurringPayment { get; set; } = new RecurringPayment();

        public ProductPrice ProductPrice { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Media Media { get; set; } = null!;

        public static PricePoint Create(string productId)
        {
            PricePoint pricePoint = new()
            {
                ProductId = productId,
                ProductPrice = ProductPrice.Create(productId, 0)
            };

            return pricePoint;
        }




        public void Set(PricePointDto pricePoint)
        {
            Header = pricePoint.Header;
            Quantity = pricePoint.Quantity;
            ImageId = pricePoint.ImageId;
            UnitPrice = pricePoint.UnitPrice;
            Unit = pricePoint.Unit;
            StrikethroughPrice = pricePoint.StrikethroughPrice;
            ProductPrice.Price = pricePoint.Price;
            ShippingType = pricePoint.ShippingType;
            RecurringPayment = pricePoint.RecurringPayment;
        }
    }
}