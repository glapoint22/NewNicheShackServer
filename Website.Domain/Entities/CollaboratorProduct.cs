using Website.Domain.Common;

namespace Website.Domain.Entities
{
    public sealed class CollaboratorProduct : Entity
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public int CollaboratorId { get; set; }
        public DateTime DateAdded { get; set; }

        public Product Product { get; set; } = null!;
        public Collaborator Collaborator { get; set; } = null!;

        public CollaboratorProduct() { }

        public CollaboratorProduct(int productId, int collaboratorId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            CollaboratorId = collaboratorId;
            DateAdded = DateTime.UtcNow;
        }
    }
}
