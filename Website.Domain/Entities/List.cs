namespace Website.Domain.Entities
{
    public class List
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CollaborateId { get; set; } = string.Empty;

        public ICollection<Collaborator> Collaborators { get; private set; } = new HashSet<Collaborator>();
    }
}