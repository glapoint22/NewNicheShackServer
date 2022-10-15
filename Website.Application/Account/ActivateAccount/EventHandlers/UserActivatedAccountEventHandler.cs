using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.ActivateAccount.EventHandlers
{
    public class UserActivatedAccountEventHandler : INotificationHandler<UserActivatedAccountEvent>
    {
        public Task Handle(UserActivatedAccountEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}