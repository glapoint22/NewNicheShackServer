namespace Shared.Common.Classes
{
    public sealed class BackgroundImage : PageImage
    {
        public KeyValuePair<string, string> Position { get; set; }
        public KeyValuePair<string, string> Repeat { get; set; }
        public KeyValuePair<string, string> Attachment { get; set; }

        internal void SetStyle(ref string styles)
        {
            if (Position.Value != null) styles += "background-position: " + Position.Value + ";";
            if (Repeat.Value != null) styles += "background-repeat: " + Repeat.Value + ";";
            if (Attachment.Value != null) styles += "background-attachment: " + Attachment.Value + ";";
        }
    }
}