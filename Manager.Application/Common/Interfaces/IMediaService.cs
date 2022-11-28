using Microsoft.AspNetCore.Http;
using Shared.Common.Enums;
using Shared.Services.MediaService.Interfaces;
using System.Drawing;

namespace Manager.Application.Common.Interfaces
{
    public interface IMediaService
    {
        string GetImagesFolder();

        Task<Image> GetImageFromFile(IFormFile imageFile);

        string CreateImageSizes(ImageSizeType imageSizeType, Image image, IMedia media);

        string UpdateImage(Image updatedImage, IMedia media, ImageSizeType imageSize);

        string AddImageSize(IMedia media, ImageSizeType imageSizeType, string src);

        Task<string?> GetVideoThumbnail(IMedia media);

        void DeleteMedia(IMedia media);
    }
}