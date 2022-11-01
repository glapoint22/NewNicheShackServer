using Website.Application.Common.Classes;

namespace Website.Infrastructure.Services.SearchSuggestionsService.Classes
{
    public sealed class SearchWordSuggestion
    {
        public string Name { get; set; } = string.Empty;
        public NicheDto? Niche { get; set; } = null!;
        public float SearchVolume { get; set; }
    }
}