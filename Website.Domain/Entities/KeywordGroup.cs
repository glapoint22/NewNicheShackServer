namespace Website.Domain.Entities
{
    public sealed class KeywordGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<KeywordInKeywordGroup> KeywordsInKeywordGroup { get; set; } = new HashSet<KeywordInKeywordGroup>();
        public ICollection<PageReferenceItem> PageReferenceItems { get; set; } = new HashSet<PageReferenceItem>();
    }
}