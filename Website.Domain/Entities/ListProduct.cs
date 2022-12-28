namespace Website.Domain.Entities
{
    public sealed class ListProduct
    {
        public string ListId { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }

        public List List { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;


        // ------------------------------------------------------------------------- Create ---------------------------------------------------------------------------
        public static ListProduct Create(string listId, Guid productId, string userId)
        {
            ListProduct listProduct = new()
            {
                ListId = listId,
                ProductId = productId,
                UserId = userId,
                DateAdded = DateTime.UtcNow
            };

            return listProduct;
        }
    }
}