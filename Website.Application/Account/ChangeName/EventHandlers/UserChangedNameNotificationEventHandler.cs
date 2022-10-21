using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeName.EventHandlers
{
    public sealed class UserChangedNameNotificationEventHandler : INotificationHandler<UserChangedNameEvent>
    {
        public Task Handle(UserChangedNameEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}