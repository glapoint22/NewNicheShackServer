using Shared.Common.Interfaces;

namespace Manager.Domain.Entities
{
    public sealed class KeywordGroupBelongingToProduct : IKeywordGroupBelongingToProduct
    {
        public string ProductId { get; set; } = string.Empty;
        public Guid KeywordGroupId { get; set; }

        public Product Product { get; set; } = null!;
        public KeywordGroup KeywordGroup { get; set; } = null!;

        public static KeywordGroupBelongingToProduct Create(Guid keywordGroupId, string productId)
        {
            KeywordGroupBelongingToProduct keywordGroupBelongingToProduct = new()
            {
                ProductId = productId,
                KeywordGroupId = keywordGroupId
            };

            return keywordGroupBelongingToProduct;
        }



        public static KeywordGroupBelongingToProduct Create(KeywordGroup keywordGroup, string productId)
        {
            KeywordGroupBelongingToProduct keywordGroupBelongingToProduct = new()
            {
                ProductId = productId,
                KeywordGroup = keywordGroup
            };

            return keywordGroupBelongingToProduct;
        }
    }
}