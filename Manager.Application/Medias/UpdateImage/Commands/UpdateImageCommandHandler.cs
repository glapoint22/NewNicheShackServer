using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Shared.Common.Classes;
using Shared.Common.Enums;

namespace Manager.Application.Medias.UpdateImage.Commands
{
    public sealed class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, Result>
    {
        private readonly IMediaService _mediaService;
        private readonly IManagerDbContext _dbContext;

        public UpdateImageCommandHandler(IMediaService mediaService, IManagerDbContext dbContext)
        {
            _mediaService = mediaService;
            _dbContext = dbContext;
        }



        public async Task<Result> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {
            string imageSrc = null!;

            // Get the Image id
            request.Form.TryGetValue("id", out StringValues idValue);
            Guid id = Guid.Parse(idValue);

            // Get the media
            Media media = (await _dbContext.Media.FindAsync(id))!;

            // Get the image
            IFormFile? imageFile = request.Form.Files["image"];
            

            if (imageFile != null)
            {
                var image = await _mediaService.GetImageFromFile(imageFile);

                // Get the image size
                request.Form.TryGetValue("imageSize", out StringValues imageSizeString);
                ImageSizeType imageSizeType = (ImageSizeType)int.Parse(imageSizeString);

                // Update the image
                imageSrc = _mediaService.UpdateImage(image, media, imageSizeType);
            }

            // Get the name
            request.Form.TryGetValue("name", out StringValues name);
            media.Name = name;

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(new
            {
                src = imageSrc
            });
        }
    }
}