using Website.Application.Common.Classes;

namespace Website.Application.Common.Interfaces
{
    public interface ISearchSuggestionsService
    {
        List<SearchSuggestion>? GetSearchSuggestions(string searchTerm, Guid nicheId);
        void SetSearchSuggestions(List<KeywordData> keywords, List<Guid> nicheIds);
    }
}