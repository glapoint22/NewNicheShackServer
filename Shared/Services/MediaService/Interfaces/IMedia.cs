namespace Shared.Services.MediaService.Interfaces
{
    public interface IMedia
    {
        Guid Id { get; set; }
        string Name { get; set; }

        string? Thumbnail { get; set; }
        int ThumbnailWidth { get; set; }
        int ThumbnailHeight { get; set; }


        string? ImageSm { get; set; }
        int ImageSmWidth { get; set; }
        int ImageSmHeight { get; set; }



        string? ImageMd { get; set; }
        int ImageMdWidth { get; set; }
        int ImageMdHeight { get; set; }


        string? ImageLg { get; set; }
        int ImageLgWidth { get; set; }
        int ImageLgHeight { get; set; }


        string? ImageAnySize { get; set; }
        int ImageAnySizeWidth { get; set; }
        int ImageAnySizeHeight { get; set; }


        string? VideoId { get; set; }
        int VideoType { get; set; }
    }
}