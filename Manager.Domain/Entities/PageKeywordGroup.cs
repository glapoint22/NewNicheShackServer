namespace Manager.Domain.Entities
{
    public sealed class PageKeywordGroup
    {
        public Guid PageId { get; set; }
        public Guid KeywordGroupId { get; set; }

        public Page Page { get; set; } = null!;
        public KeywordGroup KeywordGroup { get; set; } = null!;


        public static PageKeywordGroup Create(Guid pageId, Guid keywordGroupId)
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