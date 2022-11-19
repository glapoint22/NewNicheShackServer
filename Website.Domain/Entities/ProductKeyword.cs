namespace Website.Domain.Entities
{
    public sealed class ProductKeyword
    {
        public int Id { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public int KeywordId { get; set; }

        public Product Product { get; set; } = null!;
        public Keyword Keyword { get; set; } = null!;
    }
}