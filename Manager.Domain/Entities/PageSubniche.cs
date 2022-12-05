namespace Manager.Domain.Entities
{
    public sealed class PageSubniche
    {
        public string PageId { get; set; } = string.Empty;
        public string SubnicheId { get; set; } = string.Empty;

        public Page Page { get; set; } = null!;
        public Subniche Subniche { get; set; } = null!;

        public static PageSubniche Create(string pageId, string subnicheId)
        {
            PageSubniche pageSubniche = new()
            {
                PageId = pageId,
                SubnicheId = subnicheId
            };

            return pageSubniche;
        }
    }
}