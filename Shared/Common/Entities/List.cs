using Shared.Common.Classes;

namespace Shared.Common.Entities
{
    public sealed class List : Entity
    {
        private readonly List<Collaborator> _collaborators = new();
        private readonly List<ListProduct> _products = new();

        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string CollaborateId { get; set; } = string.Empty;

        public IReadOnlyList<Collaborator> Collaborators => _collaborators.AsReadOnly();
        public IReadOnlyList<ListProduct> Products => _products.AsReadOnly();
        public ICollection<Notification> Notifications { get; private set; } = new HashSet<Notification>();



        // --------------------------------------------------------------------- Add Collaborator ---------------------------------------------------------------------
        public bool AddCollaborator(string userId, bool isOwner)
        {
            if (_collaborators.Any(x => x.UserId == userId)) return false;

            Collaborator collaborator = Collaborator.Create(Id, userId, isOwner);
            _collaborators.Add(collaborator);

            return true;
        }





        // ------------------------------------------------------------------------- Create ---------------------------------------------------------------------------
        public static List Create(string name, string? description = null)
        {
            List list = new()
            {
                Id = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(),
                Name = name,
                Description = description,
                CollaborateId = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper()
            };


            return list;
        }





        // -------------------------------------------------------------------------- Edit ----------------------------------------------------------------------------
        public void Edit(string name, string description)
        {
            Name = name;
            Description = description;
        }







        // ----------------------------------------------------------------------- Add Product ------------------------------------------------------------------------
        public bool AddProduct(string productId, Collaborator collaborator)
        {
            string userId = collaborator.UserId;

            // Make sure this list does not contain this product
            if (_products.Any(p => p.ProductId == productId)) return false;

            // See if the collaborator has permissions to add to the list
            if (!collaborator.IsOwner && !collaborator.CanAddToList) return false;

            ListProduct listProduct = ListProduct.Create(Id, productId, userId);

            _products.Add(listProduct);

            return true;
        }





        // ---------------------------------------------------------------------- Remove Product ----------------------------------------------------------------------
        public bool RemoveProduct(string productId, Collaborator collaborator)
        {
            // See if the collaborator has permissions to remove from the list
            if (!collaborator.IsOwner && !collaborator.CanRemoveFromList) return false;

            // Get the product
            ListProduct? listProduct = _products
                .Where(p => p.ProductId == productId)
                .SingleOrDefault();

            if (listProduct == null) return false;

            _products.Remove(listProduct);

            return true;
        }
    }
}