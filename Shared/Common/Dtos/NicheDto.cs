namespace Shared.Common.Dtos
{
    public sealed class NicheDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;

        public List<SubnicheDto> Subniches { get; set; } = null!;
    }
}