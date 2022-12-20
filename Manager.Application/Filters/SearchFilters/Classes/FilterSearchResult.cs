using Manager.Application.Common.Classes;

namespace Manager.Application.Filters.SearchFilters.Classes
{
    public sealed record FilterSearchResult : SearchResult
    {
        public bool Checked { get; set; }
    }
}