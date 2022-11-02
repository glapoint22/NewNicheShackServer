
using Shared.Common.Classes;

namespace Shared.Common.Entities
{
    public sealed class CollaboratorProduct : Entity
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public int CollaboratorId { get; set; }
        public DateTime DateAdded { get; set; }

        public Product Product { get; set; } = null!;
        public Collaborator Collaborator { get; set; } = null!;

        public CollaboratorProduct() { }

        public CollaboratorProduct(string productId, int collaboratorId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            CollaboratorId = collaboratorId;
            DateAdded = DateTime.UtcNow;
        }
    }
}
