using Shared.PageBuilder.Classes;

namespace Website.Application.Common.Interfaces
{
    public interface IGridData
    {
        List<ProductDto> Products { get; }
        int TotalProducts { get; }
        int PageCount { get; }
        Filters Filters { get; }
        int ProductCountStart { get; }
        int ProductCountEnd { get; }
    }
}