namespace Manager.Domain.Entities
{
    public sealed class PageKeyword
    {
        public Guid PageId { get; set; }
        public Guid KeywordInKeywordGroupId { get; set; }

        public Page Page { get; set; } = null!;
        public KeywordInKeywordGroup KeywordInKeywordGroup { get; set; } = null!;


        public static PageKeyword Create(Guid pageId, Guid keywordInKeywordGroupId)
        {
            PageKeyword pageKeyword = new()
            {
                PageId = pageId,
                KeywordInKeywordGroupId = keywordInKeywordGroupId
            };

            return pageKeyword;
        }
    }
}