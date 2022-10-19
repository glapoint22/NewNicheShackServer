using Website.Domain.Common;

namespace Website.Domain.Entities
{
    public class CollaboratorProduct : Entity
    {
        public int ProductId { get; set; }
        public int CollaboratorId { get; set; }
        public DateTime DateAdded { get; set; }

        public Product Product { get; set; } = null!;
        public Collaborator Collaborator { get; set; } = null!;

        public CollaboratorProduct() { }

        public CollaboratorProduct(int productId, int collaboratorId)
        {
            ProductId = productId;
            CollaboratorId = collaboratorId;
            DateAdded = DateTime.UtcNow;
        }
    }
}
