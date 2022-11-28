using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Medias.PostVideo.Commands
{
    public sealed class PostVideoCommandHandler : IRequestHandler<PostVideoCommand, Result>
    {
        private readonly IMediaService _mediaService;
        private readonly IManagerDbContext _dbContext;

        public PostVideoCommandHandler(IMediaService mediaService, IManagerDbContext dbContext)
        {
            _mediaService = mediaService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(PostVideoCommand request, CancellationToken cancellationToken)
        {
            // Create the new video
            Media media = Media.CreateVideo(request.Name, request.VideoId, request.VideoType);

            // Get the thumbnail
            string? thumbnail = await _mediaService.GetVideoThumbnail(media);

            if (thumbnail == null) return Result.Failed("409");

            // Add the thumbnail to the video and save
            media.Thumbnail = thumbnail;
            _dbContext.Media.Add(media);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(new
            {
                media.Id,
                media.Thumbnail
            });
        }
    }
}