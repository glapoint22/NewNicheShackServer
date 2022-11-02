using Website.Application.Common.Classes;

namespace Website.Application.Common.Interfaces
{
    public interface ISearchSuggestionsService
    {
        List<SearchSuggestion>? GetSearchSuggestions(string searchTerm, string? nicheId);
        void SetSearchSuggestions(List<KeywordData> keywords, List<string> nicheIds);
    }
}