using Website.Application.Lists.Common;

namespace Website.Application.Lists.SharedList.Classes
{
    public record SharedListDto
    {
        public string ListId { get; init; } = string.Empty;
        public string ListName { get; init; } = string.Empty;
        public List<CollaboratorProductDto> Products { get; init; } = null!;
    }
}