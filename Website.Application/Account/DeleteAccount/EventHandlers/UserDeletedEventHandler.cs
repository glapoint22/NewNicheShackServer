using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.DeleteAccount.EventHandlers
{
    public class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
    {
        public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}