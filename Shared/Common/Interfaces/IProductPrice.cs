namespace Shared.Common.Interfaces
{
    public interface IProductPrice
    {
        Guid Id { get; set; }
        Guid ProductId { get; set; }
        double Price { get; set; }
    }
}