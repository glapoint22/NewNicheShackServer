using Shared.Common.Dtos;

namespace Shared.Common.Interfaces
{
    public interface IRepository
    {
        Task<MediaDto> GetMedia(int Id);
        Task<PageDto> GetPage(string Id);
        Task<ProductDto> GetProduct(string Id);
    }
}