using Shared.Common.Enums;
using Shared.Services.MediaService.Interfaces;

namespace Manager.Domain.Entities
{
    public sealed class Media : IMedia
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Thumbnail { get; set; }
        public int ThumbnailWidth { get; set; }
        public int ThumbnailHeight { get; set; }


        public string? ImageSm { get; set; }
        public int ImageSmWidth { get; set; }
        public int ImageSmHeight { get; set; }



        public string? ImageMd { get; set; }
        public int ImageMdWidth { get; set; }
        public int ImageMdHeight { get; set; }


        public string? ImageLg { get; set; }
        public int ImageLgWidth { get; set; }
        public int ImageLgHeight { get; set; }


        public string? ImageAnySize { get; set; }
        public int ImageAnySizeWidth { get; set; }
        public int ImageAnySizeHeight { get; set; }


        public string? VideoId { get; set; }
        public int VideoType { get; set; }


        public int MediaType { get; set; }


        public static Media CreateImage(string name)
        {
            return new Media()
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                MediaType = (int)Shared.Common.Enums.MediaType.Image
            };
        }




        public static Media CreateVideo(string name, string videoId, VideoType videoType)
        {
            return new Media()
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                VideoId = videoId,
                VideoType = (int)videoType,
                MediaType = (int)Shared.Common.Enums.MediaType.Video
            };
        }


        public void UpdateName(string name)
        {
            Name = name.Trim();
        }
    }
}