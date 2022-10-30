using Shared.Common.Entities;
using System.Linq.Expressions;

namespace Shared.Common.Interfaces
{
    public interface IRepository
    {
        IRepository<Media> Media { get; }
        IRepository<Product> Products { get; }
        IRepository<ProductFilter> ProductFilters { get; }
        IRepository<Subniche> Subniches { get; }
        IRepository<PriceRange> PriceRanges { get; }
    }


    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}