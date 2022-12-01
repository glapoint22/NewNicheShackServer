namespace Shared.Common.Interfaces
{
    public interface IProduct
    {
        string Id { get; set; }
        string Name { get; set; }
        string UrlName { get; set; }
        string SubnicheId { get; set; }
        DateTime Date { get; set; }
    }
}