using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.ImagesMedia.SaveImages.Commands
{
    public sealed class SaveImagesCommandHandler : IRequestHandler<SaveImagesCommand, Result>
    {
        private readonly IMediaService _mediaService;

        public SaveImagesCommandHandler(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        public async Task<Result> Handle(SaveImagesCommand request, CancellationToken cancellationToken)
        {
            string imagesFolder = _mediaService.GetImagesFolder();

            foreach(IFormFile imageFile in request.Images.Files)
            {
                var image = await _mediaService.GetImageFromFile(imageFile);

                string filePath = Path.Combine(imagesFolder, imageFile.FileName);
                _mediaService.SaveImage(image, filePath);
            }

            return Result.Succeeded();
        }
    }
} 