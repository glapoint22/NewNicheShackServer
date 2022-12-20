namespace Manager.Domain.Entities
{
    public sealed class ProductMedia
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid? MediaId { get; set; }
        public int Index { get; set; }

        public Product Product { get; set; } = null!;
        public Media Media { get; set; } = null!;



        public static ProductMedia Create(Guid productId, Guid MediaId, int index)
        {
            ProductMedia productMedia = new()
            {
                ProductId = productId,
                MediaId = MediaId,
                Index = index
            };

            return productMedia;
        }



        public void SetMedia(Guid mediaId)
        {
            MediaId = mediaId;
        }
    }
}