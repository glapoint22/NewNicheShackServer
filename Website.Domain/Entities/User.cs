using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Domain.Interfaces;

namespace Website.Domain.Entities
{
    public sealed class User : IdentityUser, IEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Image { get; set; }
        public bool? EmailOnNameChange { get; set; }
        public bool? EmailOnEmailChange { get; set; }
        public bool? EmailOnPasswordChange { get; set; }
        public bool? EmailOnProfileImageChange { get; set; }
        public bool? EmailOnNewCollaborator { get; set; }
        public bool? EmailOnRemovedCollaborator { get; set; }
        public bool? EmailOnRemovedListItem { get; set; }
        public bool? EmailOnMovedListItem { get; set; }
        public bool? EmailOnAddedListItem { get; set; }
        public bool? EmailOnEditedList { get; set; }
        public bool? EmailOnDeletedList { get; set; }
        public bool? EmailOnReview { get; set; }
        public string TrackingCode { get; set; } = string.Empty;
        public bool BlockNotificationSending { get; set; }

        public ICollection<Collaborator> Collaborators { get; private set; } = new HashSet<Collaborator>();
        public ICollection<Notification> Notifications { get; private set; } = new HashSet<Notification>();




        private readonly List<INotification> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();



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
    }
}