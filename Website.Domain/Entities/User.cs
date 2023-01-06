using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Domain.Entities
{
    public sealed class User : IdentityUser, IEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Image { get; set; }
        public bool? EmailOnNameUpdated { get; set; }
        public bool? EmailOnEmailUpdated { get; set; }
        public bool? EmailOnPasswordUpdated { get; set; }
        public bool? EmailOnProfileImageUpdated { get; set; }
        public bool? EmailOnCollaboratorJoinedList { get; set; }
        public bool? EmailOnUserJoinedList { get; set; }
        public bool? EmailOnUserRemovedFromList { get; set; }
        public bool? EmailOnCollaboratorRemovedFromList { get; set; }
        public bool? EmailOnUserRemovedCollaborator { get; set; }
        public bool? EmailOnCollaboratorAddedListItem { get; set; }
        public bool? EmailOnUserAddedListItem { get; set; }
        public bool? EmailOnCollaboratorRemovedListItem { get; set; }
        public bool? EmailOnUserRemovedListItem { get; set; }
        public bool? EmailOnCollaboratorMovedListItem { get; set; }
        public bool? EmailOnUserMovedListItem { get; set; }
        public bool? EmailOnCollaboratorUpdatedList { get; set; }
        public bool? EmailOnUserUpdatedList { get; set; }
        public bool? EmailOnCollaboratorDeletedList { get; set; }
        public bool? EmailOnUserDeletedList { get; set; }
        public bool? EmailOnItemReviewed { get; set; }
        public string TrackingCode { get; set; } = string.Empty;
        public bool BlockNotificationSending { get; set; }
        public int NoncompliantStrikes { get; set; }
        public bool Suspended { get; set; }

        public ICollection<Collaborator> Collaborators { get; private set; } = new HashSet<Collaborator>();
        public ICollection<Notification> Notifications { get; private set; } = new HashSet<Notification>();
        public ICollection<ProductReview> ProductReviews { get; private set; } = new HashSet<ProductReview>();




        private readonly List<INotification> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();





        // ------------------------------------------------------------------------- Add Strike ------------------------------------------------------------------------
        public void AddStrike()
        {
            NoncompliantStrikes++;
        }




        // -------------------------------------------------------------------------- Create ---------------------------------------------------------------------------
        public static User Create(string firstName, string lastName, string email)
        {
            // Create the user
            User user = new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                TrackingCode = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper()
            };


            // Create the user's first list
            string listName = firstName + (firstName.Substring(firstName.Length - 1).ToLower() == "s" ? "' List" : "'s List");
            List list = List.Create(listName);

            // Add this user as a collaborator to the list
            Collaborator collaborator = Collaborator.Create(list.Id, user.Id, true);


            user.Collaborators.Add(collaborator);
            collaborator.List = list;

            return user;
        }





        // ------------------------------------------------------------------------ Change Name ------------------------------------------------------------------------
        public void ChangeName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }





        // ------------------------------------------------------------------------ Change Image -----------------------------------------------------------------------
        public void ChangeImage(string image)
        {
            Image = image;
        }





        // ---------------------------------------------------------------------- Add Domain Event ---------------------------------------------------------------------
        public void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }







        // --------------------------------------------------------------------- Clear Domain Events ---------------------------------------------------------------------
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }




        // ------------------------------------------------------------------------ Remove Image -----------------------------------------------------------------------
        public void RemoveImage()
        {
            Image = null;
        }
    }
}