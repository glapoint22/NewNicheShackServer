using Manager.Application.Common.Interfaces;
using Shared.Common.Dtos;
using Shared.Common.Interfaces;
using Shared.Services.MediaService.Interfaces;
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

        public Task<PageDto> GetPage(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetProduct(string Id)
        {
            throw new NotImplementedException();
        }
    }
}