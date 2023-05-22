using Manager.Application.Common.Interfaces;
using Website.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using IAuthService = Manager.Application.Common.Interfaces.IAuthService;
using IMediaService = Manager.Application.Common.Interfaces.IMediaService;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using Shared.Common.Enums;

namespace Manager.Application._Publish.Common.Classes
{
    public abstract class Publish
    {
        protected readonly IManagerDbContext _managerDbContext;
        protected readonly IWebsiteDbContext _websiteDbContext;
        private readonly IMediaService _mediaService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        protected Publish(IWebsiteDbContext websiteDbContext, IManagerDbContext managerDbContext, IMediaService mediaService, IAuthService authService, IConfiguration configuration)
        {
            _websiteDbContext = websiteDbContext;
            _managerDbContext = managerDbContext;
            _mediaService = mediaService;
            _authService = authService;
            _configuration = configuration;
        }




        protected static List<Guid> GetMediaIds(string content)
        {
            Regex regex = new(@"""image"":{""id"":""([a-zA-Z0-9-]+)""");
            MatchCollection matches = regex.Matches(content);

            return matches
                .Select(x => new Guid(x.Groups[1].Value))
                .ToList();
        }






        protected async Task PostImages(List<Guid> mediaIds)
        {
            // Get media from manager and add it to website
            List<Website.Domain.Entities.Media> media = await _managerDbContext.Media
            .Where(x => mediaIds.Contains(x.Id))
            .Select(x => new Website.Domain.Entities.Media
            {
                Id = x.Id,
                Name = x.Name,
                Thumbnail = x.MediaType == (int)MediaType.Video || x.ImageMd != null ? x.Thumbnail : null,
                ImageSm = x.ImageSm,
                ImageMd = x.ImageMd,
                ImageLg = x.ImageLg,
                ImageAnySize = x.ImageAnySize,
                VideoId = x.VideoId,
                MediaType = x.MediaType,
                VideoType = x.VideoType
            }).ToListAsync();

            _websiteDbContext.Media.AddRange(media);


            List<string> images = new();

            foreach (var m in media)
            {
                if (m.Thumbnail != null && !images.Contains(m.Thumbnail)) images.Add(m.Thumbnail);
                if (m.ImageSm != null && !images.Contains(m.ImageSm)) images.Add(m.ImageSm);
                if (m.ImageMd != null && !images.Contains(m.ImageMd)) images.Add(m.ImageMd);
                if (m.ImageLg != null && !images.Contains(m.ImageLg)) images.Add(m.ImageLg);
                if (m.ImageAnySize != null && !images.Contains(m.ImageAnySize)) images.Add(m.ImageAnySize);
            }

            HttpResponseMessage result = await _mediaService.PostImages(images, _configuration["Website:Images"], _authService.GetAccessTokenFromHeader()!);
        }
    }
}