namespace Website.Domain.Entities
{
    public class ListProduct
    {
        public int ProductId { get; set; }
        public int CollaboratorId { get; set; }
        public DateTime DateAdded { get; set; }

        public Product Product { get; set; } = null!;
        public ListCollaborator Collaborator { get; set; } = null!;
    }
}
