﻿namespace Website.Domain.Entities
{
    public sealed class ProductKeyword
    {
        public Guid ProductId { get; set; }
        public Guid KeywordId { get; set; }

        public Product Product { get; set; } = null!;
        public Keyword Keyword { get; set; } = null!;
    }
}