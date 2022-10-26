namespace Shared.Common.Classes
{
    public sealed class Background
    {
        public string Color { get; set; } = string.Empty;
        public BackgroundImage Image { get; set; } = null!;
        public bool Enabled { get; set; }
    }
}