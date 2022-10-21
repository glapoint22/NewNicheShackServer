namespace Website.Domain.Entities
{
    public sealed class Page
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int PageType { get; set; }
    }
}