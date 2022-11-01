namespace Shared.Common.Entities
{
    public sealed class ProductKeyword
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int KeywordId { get; set; }

        public Product Product { get; set; } = null!;
        public Keyword Keyword { get; set; } = null!;
    }
}