using Shared.Common.Dtos;
using Shared.Services.MediaService.Interfaces;
using System.Linq.Expressions;

namespace Shared.Common.Interfaces
{
    public interface IRepository
    {
        IQueryable<IMedia> Media(Expression<Func<IMedia, bool>> predicate);
        Task<PageDto> GetPage(string Id);
        Task<ProductDto> GetProduct(string Id);
    }
}