namespace Website.Domain.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string ImageSm { get; set; } = string.Empty;
        public string ImageMd { get; set; } = string.Empty;
        public string ImageLg { get; set; } = string.Empty;
        public string VideoId { get; set; } = string.Empty;
        public int MediaType { get; set; }
        public int VideoType { get; set; }
    }
}