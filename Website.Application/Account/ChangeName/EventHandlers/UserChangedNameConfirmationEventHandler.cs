using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeName.EventHandlers
{
    public sealed class UserChangedNameConfirmationEventHandler : INotificationHandler<UserChangedNameEvent>
    {
        public Task Handle(UserChangedNameEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}