using Shared.Common.Dtos;

namespace Shared.Common.Interfaces
{
    public interface IRepository
    {
        Task<MediaDto> GetMedia(int id);

    }
}