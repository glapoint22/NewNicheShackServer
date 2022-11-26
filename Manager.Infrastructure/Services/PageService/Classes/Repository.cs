using Shared.Common.Dtos;
using Shared.Common.Interfaces;

namespace Manager.Infrastructure.Services.PageService.Classes
{
    public sealed class Repository : IRepository
    {
        public Task<MediaDto> GetMedia(int Id)
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