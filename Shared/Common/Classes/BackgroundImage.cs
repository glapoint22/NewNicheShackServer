namespace Shared.Common.Classes
{
    public sealed class BackgroundImage : Image
    {
        public KeyValuePair<string, string> Position { get; set; }
        public KeyValuePair<string, string> Repeat { get; set; }
        public KeyValuePair<string, string> Attachment { get; set; }
    }
}