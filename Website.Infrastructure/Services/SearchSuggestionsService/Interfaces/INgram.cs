namespace Website.Infrastructure.Services.SearchSuggestionsService.Interfaces
{
    public interface INgram
    {
        List<string> ToList();
        string ToSearchTerm();
    }
}