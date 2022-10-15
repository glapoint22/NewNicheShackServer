using MediatR;

namespace Website.Domain.Common
{
    public interface IEntity
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }

        void AddDomainEvent(INotification domainEvent);

        void ClearDomainEvents();
    }
}