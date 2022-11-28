using Shared.Common.Dtos;
using Shared.Common.Interfaces;
using Shared.Services.MediaService.Interfaces;
using System.Linq.Expressions;

namespace Website.Infrastructure.Services.PageService.Classes
{
    public sealed class Repository : IRepository
    {
        public IQueryable<IMedia> Media(Expression<Func<IMedia, bool>> predicate)
        {
            throw new NotImplementedException();
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