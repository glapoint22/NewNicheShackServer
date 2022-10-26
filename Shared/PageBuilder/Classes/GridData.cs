namespace Shared.PageBuilder.Classes
{
    public sealed class GridData
    {
        public List<QueriedProduct> Products { get; set; } = new List<QueriedProduct>();
        public int TotalProducts { get; set; }
        public double PageCount { get; set; }
        //public Filters Filters { get; set; }
        public double ProductCountStart { get; set; }
        public double ProductCountEnd { get; set; }

        //public async Task<GridData> GetData(SharedDbContext dbContext)
        //{

        //}
    }
}