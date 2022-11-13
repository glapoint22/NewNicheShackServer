using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeName.EventHandlers
{
    public sealed class UserChangedNameEventHandler : INotificationHandler<UserChangedNameEvent>
    {
        public Task Handle(UserChangedNameEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}