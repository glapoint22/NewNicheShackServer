namespace Shared.Services.MediaService.Classes
{
    public sealed class Wistia
    {
        public string Version { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public string html { get; set; } = string.Empty;
        public int width { get; set; }
        public int height { get; set; }
        public string provider_name { get; set; } = string.Empty;
        public string provider_url { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string thumbnail_url { get; set; } = string.Empty;
        public int thumbnail_width { get; set; }
        public int thumbnail_height { get; set; }
        public float duration { get; set; }
    }
}