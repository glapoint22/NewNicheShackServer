using Shared.Common.Dtos;

namespace Shared.PageBuilder.Widgets.GridWidget.Classes
{
    public abstract class GridData
    {
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public int TotalProducts { get; set; }
        public int PageCount { get; set; }
        public Filters Filters { get; set; } = null!;
        public int ProductCountStart { get; set; }
        public int ProductCountEnd { get; set; }

        protected const int _limit = 40;
    }
}
