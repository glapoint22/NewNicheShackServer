﻿using Shared.Common.Classes;

namespace Website.Domain.Entities
{
    public sealed class List : Entity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string CollaborateId { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }


        private readonly List<Collaborator> _collaborators = new();
        public IReadOnlyList<Collaborator> Collaborators => _collaborators.AsReadOnly();

        private readonly List<ListProduct> _products = new();
        public IReadOnlyList<ListProduct> Products => _products.AsReadOnly();



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
                CollaborateId = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(),
                CreationDate = DateTime.UtcNow,
            };


            return list;
        }





        // -------------------------------------------------------------------------- Edit ----------------------------------------------------------------------------
        public void Edit(string name, string? description)
        {
            Name = name;
            Description = description;
        }







        // ----------------------------------------------------------------------- Add Product ------------------------------------------------------------------------
        public bool AddProduct(Guid productId, Collaborator collaborator)
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




        public void ReformList(int option)
        {
            switch (option)
            {
                case 0:
                    Name = "My List";
                    break;

                case 1:
                    Description = null;
                    break;

                case 2:
                    Name = "My List";
                    Description = null;
                    break;
            }
        }





        // ---------------------------------------------------------------------- Remove Product ----------------------------------------------------------------------
        public bool RemoveProduct(Guid productId, Collaborator collaborator)
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