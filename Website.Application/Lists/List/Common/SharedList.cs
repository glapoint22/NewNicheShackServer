namespace Website.Application.Lists.List.Common
{
    public record SharedList
    {
        public string ListId { get; init; } = string.Empty;
        public string ListName { get; init; } = string.Empty;
        public string UrlName { get; init; } = string.Empty;
        public List<ListProductDto> Products { get; init; } = null!;
    }
}