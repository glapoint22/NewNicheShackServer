namespace Shared.Common.Interfaces
{
    public interface IProductInProductGroup
    {
        Guid Id { get; set; }
        string ProductId { get; set; }
        Guid ProductGroupId { get; set; }
    }
}