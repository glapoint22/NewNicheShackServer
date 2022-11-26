using Shared.Common.Interfaces;

namespace Shared.PageBuilder.Classes
{
    public sealed class Video
    {
        public int Id { get; set; }
        public string Thumbnail { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int VideoType { get; set; }
        public string VideoId { get; set; } = string.Empty;


        public async Task SetData(IRepository repository)
        {
        }
    }
}