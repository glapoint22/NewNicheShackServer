using Manager.Application.Common.Interfaces;
using Shared.Common.Interfaces;
using System.Linq.Expressions;

namespace Manager.Infrastructure.Services.PageService.Classes
{
    public sealed class Repository : IRepository
    {
        private readonly IManagerDbContext _dbContext;

        public Repository(IManagerDbContext dbContext)
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