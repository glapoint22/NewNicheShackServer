namespace Shared.Common.Interfaces
{
    public interface IProduct
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string UrlName { get; set; }
        Guid SubnicheId { get; set; }
        DateTime Date { get; set; }
    }
}