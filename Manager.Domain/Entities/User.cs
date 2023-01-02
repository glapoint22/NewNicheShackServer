using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager.Domain.Entities
{
    public sealed class User : IdentityUser, IEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Image { get; set; }

        public ICollection<NotificationEmployeeNote> NotificationEmployeeNotes { get; private set; } = new HashSet<NotificationEmployeeNote>();


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
    }
}