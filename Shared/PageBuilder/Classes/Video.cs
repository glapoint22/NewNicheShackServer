using Microsoft.EntityFrameworkCore;
using Shared.Common.Interfaces;

namespace Shared.PageBuilder.Classes
{
    public sealed class Video
    {
        public Guid Id { get; set; }
        public string Thumbnail { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int VideoType { get; set; }
        public string VideoId { get; set; } = string.Empty;


        public async Task SetData(IRepository repository)
        {
            var video = await repository.Media(x => x.Id == Id)
                .Select(x => new
                {
                    x.Name,
                    x.Thumbnail,
                    x.VideoType,
                    x.VideoId
                })
                .SingleAsync();

            Name = video.Name;
            Thumbnail = video.Thumbnail!;
            VideoType = video.VideoType;
            VideoId = video.VideoId!;
        }
    }
}