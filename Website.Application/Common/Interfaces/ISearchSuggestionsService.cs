using Website.Application.Common.Classes;

namespace Website.Application.Common.Interfaces
{
    public interface ISearchSuggestionsService
    {
        List<SearchSuggestion>? GetSearchSuggestions(string searchTerm, int? nicheId);
        void SetSearchSuggestions(List<KeywordData> keywords, List<int> nicheIds);
    }
}