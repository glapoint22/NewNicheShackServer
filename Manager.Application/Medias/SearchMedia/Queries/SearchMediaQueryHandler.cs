using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Medias.SearchMedia.Queries
{
    public sealed class SearchMediaQueryHandler : IRequestHandler<SearchMediaQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchMediaQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchMediaQuery request, CancellationToken cancellationToken)
        {
            var mediaList = await _dbContext.Media
                .Where(x => x.MediaType == request.MediaType)
                .Where(request.SearchTerm)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Thumbnail,
                    x.ThumbnailWidth,
                    x.ThumbnailHeight,
                    x.ImageSm,
                    x.ImageSmWidth,
                    x.ImageSmHeight,
                    x.ImageMd,
                    x.ImageMdWidth,
                    x.ImageMdHeight,
                    x.ImageLg,
                    x.ImageLgWidth,
                    x.ImageLgHeight,
                    x.ImageAnySize,
                    x.ImageAnySizeWidth,
                    x.ImageAnySizeHeight,
                    x.VideoId,
                    x.VideoType
                }).ToListAsync();

            return Result.Succeeded(mediaList);
        }
    }
}