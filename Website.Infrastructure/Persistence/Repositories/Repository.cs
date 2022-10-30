using Microsoft.EntityFrameworkCore;
using Shared.Common.Entities;
using Shared.Common.Interfaces;
using System.Linq.Expressions;
using Website.Application.Common.Interfaces;

namespace Website.Infrastructure.Persistence.Repositories
{
    public class Repository : IRepository
    {
        public IRepository<Media> Media { get; } = null!;
        public IRepository<Product> Products { get; } = null!;
        public IRepository<ProductFilter> ProductFilters { get; } = null!;
        public IRepository<Subniche> Subniches { get; } = null!;
        public IRepository<PriceRange> PriceRanges { get; } = null!;

        public Repository(IWebsiteDbContext dbContext)
        {
            Media = new Repository<Media>((DbContext)dbContext);
            Products = new Repository<Product>((DbContext)dbContext);
            ProductFilters = new Repository<ProductFilter>((DbContext)dbContext);
            Subniches = new Repository<Subniche>((DbContext)dbContext);
            PriceRanges = new Repository<PriceRange>((DbContext)dbContext);
        }
    }




    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        public Repository(DbContext context)
        {
            _dbContext = context;
        }

        public async Task<T> Get(int id)
        {
            return (await _dbContext.Set<T>()
                .FindAsync(id))!;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>()
                .ToListAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }
    }
}