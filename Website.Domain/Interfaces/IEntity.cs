using MediatR;

namespace Website.Domain.Interfaces
{
    public interface IEntity
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }

        void AddDomainEvent(INotification domainEvent);

        void ClearDomainEvents();
    }
}