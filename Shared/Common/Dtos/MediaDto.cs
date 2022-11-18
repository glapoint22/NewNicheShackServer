namespace Shared.Common.Dtos
{
    public sealed class MediaDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Thumbnail { get; set; } = string.Empty;
        public string? ImageSm { get; set; } = string.Empty;
        public string? ImageMd { get; set; } = string.Empty;
        public string? ImageLg { get; set; } = string.Empty;
        public string? ImageAnySize { get; set; } = string.Empty;
        public string? VideoId { get; set; } = string.Empty;
        public int VideoType { get; set; }
    }
}