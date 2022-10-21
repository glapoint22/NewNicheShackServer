using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeProfileImage.EventHandlers
{
    public sealed class UserChangedProfileImageConfirmationEventHandler : INotificationHandler<UserChangedProfileImageEvent>
    {
        public Task Handle(UserChangedProfileImageEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}