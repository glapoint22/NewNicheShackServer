namespace Shared.Common.Entities
{
    public sealed class Page
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? UrlName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int PageType { get; set; }

        private readonly List<PageReferenceItem> _pageReferenceItems = new();
        public IReadOnlyList<PageReferenceItem> PageReferenceItems => _pageReferenceItems.AsReadOnly();
    }
}