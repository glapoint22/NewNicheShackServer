using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeEmail.EventHandlers
{
    public sealed class UserChangedEmailEventHandler : INotificationHandler<UserChangedEmailEvent>
    {
        public Task Handle(UserChangedEmailEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}