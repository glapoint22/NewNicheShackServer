namespace Shared.Common.Interfaces
{
    public interface IProductInProductGroup
    {
        string ProductId { get; set; }
        Guid ProductGroupId { get; set; }
    }
}