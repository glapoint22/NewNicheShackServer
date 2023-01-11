using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Website.Application.Common.Interfaces
{
    public interface IMediaService
    {
        string GetImagesFolder();

        Task<Image> GetImageFromFile(IFormFile imageFile);

        void SaveImage(Image image, string path);
    }
}