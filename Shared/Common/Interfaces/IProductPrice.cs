namespace Shared.Common.Interfaces
{
    public interface IProductPrice
    {
        Guid Id { get; set; }
        string ProductId { get; set; }
        double Price { get; set; }
    }
}