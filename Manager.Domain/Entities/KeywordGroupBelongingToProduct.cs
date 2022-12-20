using Shared.Common.Interfaces;

namespace Manager.Domain.Entities
{
    public sealed class KeywordGroupBelongingToProduct : IKeywordGroupBelongingToProduct
    {
        public Guid ProductId { get; set; }
        public Guid KeywordGroupId { get; set; }

        public Product Product { get; set; } = null!;
        public KeywordGroup KeywordGroup { get; set; } = null!;

        public static KeywordGroupBelongingToProduct Create(Guid keywordGroupId, Guid productId)
        {
            KeywordGroupBelongingToProduct keywordGroupBelongingToProduct = new()
            {
                ProductId = productId,
                KeywordGroupId = keywordGroupId
            };

            return keywordGroupBelongingToProduct;
        }



        public static KeywordGroupBelongingToProduct Create(KeywordGroup keywordGroup, Guid productId)
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