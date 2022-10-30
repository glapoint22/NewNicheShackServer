using MediatR;

namespace Shared.Common.Interfaces
{
    public interface IEntity
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }

        void AddDomainEvent(INotification domainEvent);

        void ClearDomainEvents();
    }
}