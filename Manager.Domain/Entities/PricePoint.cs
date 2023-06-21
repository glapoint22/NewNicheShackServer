using Manager.Domain.Dtos;

namespace Manager.Domain.Entities
{
    public sealed class PricePoint
    {
        public Guid Id { get; set; }
        public Guid ProductPriceId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? ImageId { get; set; }
        public string? Header { get; set; }
        public string? Subheader { get; set; }
        public string? Quantity { get; set; }
        public int ShippingType { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Info { get; set; }
        public string? ShippingValue { get; set; }


        public ProductPrice ProductPrice { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Media Media { get; set; } = null!;

        public static PricePoint Create(Guid productId)
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
            Subheader = pricePoint.Subheader;
            Quantity = pricePoint.Quantity;
            ImageId = pricePoint.ImageId;
            ProductPrice.Price = pricePoint.Price;
            ShippingType = pricePoint.ShippingType;
            Text = pricePoint.Text;
            Info = pricePoint.Info;
            ShippingValue = pricePoint.ShippingValue;
        }
    }
}