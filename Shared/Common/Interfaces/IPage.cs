namespace Shared.Common.Interfaces
{
    public interface IPage
    {
        string Id { get; set; }
        string Name { get; set; }
        string? UrlName { get; set; }
        int PageType { get; set; }
    }
}