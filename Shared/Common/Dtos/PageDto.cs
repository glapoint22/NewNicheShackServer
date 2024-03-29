﻿namespace Shared.Common.Dtos
{
    public sealed class PageDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? UrlName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int PageType { get; set; }
    }
}