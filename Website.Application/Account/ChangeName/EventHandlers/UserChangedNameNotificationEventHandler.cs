using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeName.EventHandlers
{
    internal class UserChangedNameNotificationEventHandler : INotificationHandler<UserChangedNameEvent>
    {
        public Task Handle(UserChangedNameEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}