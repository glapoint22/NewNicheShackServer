using Shared.Common.Classes;

namespace Shared.Common.Entities
{
    public sealed class List : Entity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string CollaborateId { get; set; } = string.Empty;

        public ICollection<Collaborator> Collaborators { get; private set; } = new HashSet<Collaborator>();


        public List() { }

        public List(string listName, string? description = null)
        {
            Id = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
            Name = listName;
            Description = description;
            CollaborateId = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }


        public void Edit(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}