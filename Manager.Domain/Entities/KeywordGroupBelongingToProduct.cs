namespace Manager.Domain.Entities
{
    public sealed class KeywordGroupBelongingToProduct
    {
        public string ProductId { get; set; } = string.Empty;
        public Guid KeywordGroupId { get; set; }

        public Product Product { get; set; } = null!;
        public KeywordGroup KeywordGroup { get; set; } = null!;
    }
}