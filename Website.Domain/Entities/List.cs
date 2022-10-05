namespace Website.Domain.Entities
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CollaborateId { get; set; } = string.Empty;

        public ICollection<ListCollaborator> Collaborators { get; private set; } = new HashSet<ListCollaborator>();
    }
}
