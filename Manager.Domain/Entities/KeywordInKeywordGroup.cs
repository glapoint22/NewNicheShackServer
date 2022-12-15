namespace Manager.Domain.Entities
{
    public sealed class KeywordInKeywordGroup
    {
        public Guid Id { get; set; }
        public Guid KeywordGroupId { get; set; }
        public Guid KeywordId { get; set; }

        public KeywordGroup KeywordGroup { get; set; } = null!;
        public Keyword Keyword { get; set; } = null!;

        public ICollection<PageKeyword> PageKeywords { get; private set; } = new HashSet<PageKeyword>();

        public static KeywordInKeywordGroup Create(Keyword keyword, Guid keywordGroupId)
        {
            KeywordInKeywordGroup keywordInKeywordGroup = new()
            {
                Keyword = keyword,
                KeywordGroupId = keywordGroupId
            };

            return keywordInKeywordGroup;
        }
    }
}