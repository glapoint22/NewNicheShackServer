namespace Manager.Domain.Entities
{
    public sealed class ProductKeyword
    {
        public Guid ProductId { get; set; }
        public Guid KeywordId { get; set; }

        public Product Product { get; set; } = null!;
        public Keyword Keyword { get; set; } = null!;

        public static ProductKeyword Create(Guid productId, Guid keywordId)
        {
            ProductKeyword productKeyword = new()
            {
                ProductId = productId,
                KeywordId = keywordId
            };

            return productKeyword;
        }



        public static ProductKeyword Create(Guid productId, Keyword keyword)
        {
            ProductKeyword productKeyword = new()
            {
                ProductId = productId,
                Keyword = keyword
            };

            return productKeyword;
        }
    }
}