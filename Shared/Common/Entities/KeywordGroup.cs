namespace Shared.Common.Entities
{
    public sealed class KeywordGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<KeywordInKeywordGroup> KeywordsInKeywordGroup { get; set; } = new HashSet<KeywordInKeywordGroup>();
        private readonly List<PageReferenceItem> _pageReferenceItems = new();
        public IReadOnlyList<PageReferenceItem> PageReferenceItems => _pageReferenceItems.AsReadOnly();
    }
}