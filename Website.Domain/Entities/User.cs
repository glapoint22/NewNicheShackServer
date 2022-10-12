using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Domain.Common;
using Website.Domain.Events;

namespace Website.Domain.Entities
{
    public class User : IdentityUser, IEntity
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
        public bool? EmailOnListNameChange { get; set; }
        public bool? EmailOnDeletedList { get; set; }
        public bool? EmailOnReview { get; set; }



        private readonly List<INotification> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();




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




        // ------------------------------------------------------------------------ Create User --------------------------------------------------------------------------
        public static User CreateUser(string firstName, string lastName, string email)
        {
            User user = new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email
            };

            user.AddDomainEvent(new UserCreatedEvent(user));

            return user;
        }
    }
}