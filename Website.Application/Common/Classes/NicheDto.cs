namespace Website.Application.Common.Classes
{
    public sealed class NicheDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;

        public List<SubnicheDto> Subniches { get; set; } = null!;
    }
}