﻿namespace Manager.Domain.Entities
{
    public sealed class ProductKeyword
    {
        public string ProductId { get; set; } = string.Empty;
        public Guid KeywordId { get; set; }

        public Product Product { get; set; } = null!;
        public Keyword Keyword { get; set; } = null!;
    }
}