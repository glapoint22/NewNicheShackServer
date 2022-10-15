using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.ChangePassword.EventHandlers
{
    public class UserChangedPasswordEventHandler : INotificationHandler<UserChangedPasswordEvent>
    {
        public Task Handle(UserChangedPasswordEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}