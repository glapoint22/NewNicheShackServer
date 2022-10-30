namespace Shared.PageBuilder.Classes
{
    public sealed class Filters
    {
        public List<NicheFilter> NicheFilters { get; set; } = null!;
        public PriceFilter PriceFilter { get; set; } = null!;
        public QueryFilter RatingFilter { get; set; } = null!;
        public List<QueryFilter> CustomFilters { get; set; } = null!;
    }
}