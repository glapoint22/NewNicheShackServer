﻿using Shared.Common.Interfaces;

namespace Manager.Domain.Entities
{
    public sealed class KeywordGroupBelongingToProduct : IKeywordGroupBelongingToProduct
    {
        public string ProductId { get; set; } = string.Empty;
        public Guid KeywordGroupId { get; set; }

        public Product Product { get; set; } = null!;
        public KeywordGroup KeywordGroup { get; set; } = null!;
    }
}