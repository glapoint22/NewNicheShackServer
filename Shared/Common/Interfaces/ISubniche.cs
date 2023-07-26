namespace Shared.Common.Interfaces
{
    public interface ISubniche
    {
        Guid Id { get; set; }
        Guid NicheId { get; set; }
        string Name { get; set; }
        string UrlName { get; set; }
    }
}