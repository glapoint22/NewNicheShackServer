namespace Shared.Services.MediaService.Classes
{
    public sealed class Vimeo
    {
        public string type { get; set; } = string.Empty;
        public string version { get; set; } = string.Empty;
        public string provider_name { get; set; } = string.Empty;
        public string provider_url { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string author_name { get; set; } = string.Empty;
        public string author_url { get; set; } = string.Empty;
        public string account_type { get; set; } = string.Empty;
        public string html { get; set; } = string.Empty;
        public int width { get; set; }
        public int height { get; set; }
        public int duration { get; set; }
        public string description { get; set; } = string.Empty;
        public string thumbnail_url { get; set; } = string.Empty;
        public int thumbnail_width { get; set; }
        public int thumbnail_height { get; set; }
        public string thumbnail_url_with_play_button { get; set; } = string.Empty;
        public string upload_date { get; set; } = string.Empty;
        public int video_id { get; set; }
        public string uri { get; set; } = string.Empty;
    }
}