using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Medias.UpdateVideo.Commands
{
    public sealed class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IMediaService _mediaService;

        public UpdateVideoCommandHandler(IManagerDbContext dbContext, IMediaService mediaService)
        {
            _dbContext = dbContext;
            _mediaService = mediaService;
        }

        public async Task<Result> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            // Get the current video
            Media media = (await _dbContext.Media.FindAsync(request.Id))!;

            if (request.VideoId != null)
            {
                media.VideoId = request.VideoId;
                media.VideoType = request.VideoType;
                string? thumbnail = await _mediaService.GetVideoThumbnail(media);

                // If we don't have a thumbnail, return failed
                if (thumbnail == null) return Result.Failed("409");

                // Delete the old thumbnail
                string imagesFolder = _mediaService.GetImagesFolder();
                File.Delete(Path.Combine(imagesFolder, media.Thumbnail!));

                media.Thumbnail = thumbnail;
            }

            media.Name = request.Name;

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(new
            {
                media.Thumbnail
            });
        }
    }
}