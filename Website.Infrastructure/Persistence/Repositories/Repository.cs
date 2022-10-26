using Microsoft.EntityFrameworkCore;
using Shared.Common.Dtos;
using Shared.Common.Interfaces;
using Website.Application.Common.Interfaces;

namespace Website.Infrastructure.Persistence.Repositories
{
    public class Repository : IRepository
    {
        private readonly IWebsiteDbContext _dbContext;

        public Repository(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        // ------------------------------------------------------------------------- Get Media ------------------------------------------------------------------------
        public async Task<MediaDto> GetMedia(int id)
        {
            return await _dbContext.Media
                .Where(x => x.Id == id)
                .Select(x => new MediaDto
                {
                    Name = x.Name,
                    Thumbnail = x.Thumbnail,
                    ImageSm = x.ImageSm,
                    ImageMd = x.ImageMd,
                    ImageLg = x.ImageLg,
                    VideoId = x.VideoId,
                    VideoType = x.VideoType
                })
                .SingleAsync();
        }
    }
}