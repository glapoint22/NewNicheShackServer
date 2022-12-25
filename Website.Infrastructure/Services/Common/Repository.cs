using Shared.Common.Interfaces;
using System.Linq.Expressions;
using Website.Application.Common.Interfaces;

namespace Website.Infrastructure.Services.Common
{
    public sealed class Repository : IRepository
    {
        private readonly IWebsiteDbContext _dbContext;

        public Repository(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<IMedia> Media(Expression<Func<IMedia, bool>> predicate)
        {
            return _dbContext.Media
                .Where(predicate);
        }

        public IQueryable<IPage> Pages(Expression<Func<IPage, bool>> predicate)
        {
            return _dbContext.Pages
                .Where(predicate);
        }

        public IQueryable<IProduct> Products(Expression<Func<IProduct, bool>> predicate)
        {
            return _dbContext.Products
                .Where(predicate);
        }
    }
}