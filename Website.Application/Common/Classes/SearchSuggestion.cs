namespace Website.Application.Common.Classes
{
    public sealed class SearchSuggestion
    {
        public string Name { get; set; } = string.Empty;
        public NicheDto Niche { get; set; } = null!;
    }
}