using Website.Application.Common.Classes;

namespace Website.Application.Lists.Common
{
    public sealed class CollaboratorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Image ProfileImage { get; set; } = null!;
    };
}