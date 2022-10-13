using Website.Application.Account.Common.EventHandlers;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeProfileImage.EventHandlers
{
    public class UserChangedImageEventHandler : UserUpdatedEventHandler<UserChangedImageEvent>
    {
        public UserChangedImageEventHandler(IUserService userService, ICookieService cookieService) : base(userService, cookieService)
        {
        }

        public override async Task Handle(UserChangedImageEvent notification, CancellationToken cancellationToken)
        {
            await base.Handle(notification, cancellationToken);

            if (notification.User.EmailOnProfileImageChange == true)
            {
                // TODO: Send email
            }
        }
    }
}