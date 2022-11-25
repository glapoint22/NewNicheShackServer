using Shared.Common.Dtos;
using Website.Application.Common.Classes;

namespace Website.Infrastructure.Services.SearchSuggestionsService.Classes
{
    public sealed class SplitSearchTerm
    {
        public string SearchTerm { get; set; } = string.Empty;
        public List<NicheDto>? Niches { get; set; } = new List<NicheDto>();
        public float SearchVolume { get; set; }
        public List<SearchTerm>? Parents { get; set; } = new List<SearchTerm>();
    }
}