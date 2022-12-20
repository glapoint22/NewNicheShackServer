namespace Shared.Common.Interfaces
{
    public interface IProductInProductGroup
    {
        Guid ProductId { get; set; }
        Guid ProductGroupId { get; set; }
    }
}