using Shared.Common.Interfaces;
using System.Linq.Expressions;

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

        private readonly IRepository _repository;

        public GridData(IRepository repository)
        {
            _repository = repository;
        }


        public async Task SetData<T>(Expression<Func<T, bool>> query)
        {
            var a = 0;
        }
    }
}