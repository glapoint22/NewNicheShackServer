using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Primitives;
using Shared.Common.Classes;
using Shared.Common.Enums;

namespace Manager.Application.Medias.PostNewImage.Commands
{
    public sealed class PostImageCommandHandler : IRequestHandler<PostImageCommand, Result>
    {
        private readonly IMediaService _mediaService;
        private readonly IManagerDbContext _dbContext;

        public PostImageCommandHandler(IMediaService mediaService, IManagerDbContext dbContext)
        {
            _mediaService = mediaService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(PostImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _mediaService.GetImageFromFile(request.Form.Files["image"]!);

            // Get the image name
            request.Form.TryGetValue("name", out StringValues imageName);

            // Create the media image
            Media media = Media.CreateImage(imageName.ToString());

            // Get the image size
            request.Form.TryGetValue("imageSize", out StringValues imageSizeType);

            // Create the image sizes for this image
            string imageSrc = _mediaService.CreateImageSizes((ImageSizeType)int.Parse(imageSizeType), image, media);

            _dbContext.Media.Add(media);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(new
            {
                media.Id,
                Src = imageSrc,
                media.Name,
                media.Thumbnail
            });
        }
    }
}