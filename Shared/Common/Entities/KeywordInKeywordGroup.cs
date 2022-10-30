namespace Shared.Common.Entities
{
    public sealed class KeywordInKeywordGroup
    {
        public int Id { get; set; }
        public int KeywordGroupId { get; set; }
        public int KeywordId { get; set; }

        public KeywordGroup KeywordGroup { get; set; } = null!;
        public Keyword Keyword { get; set; } = null!;
    }
}