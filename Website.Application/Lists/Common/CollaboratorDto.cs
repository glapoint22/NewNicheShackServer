using Website.Application.Common.Classes;

namespace Website.Application.Lists.Common
{
    public class CollaboratorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ImageDto ProfileImage { get; set; } = null!;
    }
}