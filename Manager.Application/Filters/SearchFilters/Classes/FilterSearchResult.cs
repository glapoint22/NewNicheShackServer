﻿namespace Manager.Application.Filters.SearchFilters.Classes
{
    public sealed class FilterSearchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool Checked { get; set; }
    }
}