using Manager.Domain.Classes;

namespace Manager.Domain.Entities
{
    public sealed class Page
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? UrlName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int PageType { get; set; }

        public void Update(string name, string content, int pageType)
        {
            Name = name;
            Content = content;
            PageType = pageType;

            if (PageType == (int)Shared.PageBuilder.Enums.PageType.Custom || PageType == (int)Shared.PageBuilder.Enums.PageType.Browse)
            {
                UrlName = Utility.GenerateUrlName(name);
            }
            else
            {
                UrlName = null;
            }
        }
    }
}