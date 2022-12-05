namespace Manager.Domain.Entities
{
    public sealed class PageKeywordGroup
    {
        public string PageId { get; set; } = string.Empty;
        public Guid KeywordGroupId { get; set; }

        public Page Page { get; set; } = null!;
        public KeywordGroup KeywordGroup { get; set; } = null!;


        public static PageKeywordGroup Create(string pageId, Guid keywordGroupId)
        {
            PageKeywordGroup pageKeywordGroup = new()
            {
                PageId = pageId,
                KeywordGroupId = keywordGroupId
            };

            return pageKeywordGroup;
        }
    }
}