using Website.Application.Account.Common.EventHandlers;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeEmail.EventHandlers
{
    public class UserChangedEmailEventHandler : UserUpdatedEventHandler<UserChangedEmailEvent>
    {
        public UserChangedEmailEventHandler(IUserService userService, ICookieService cookieService) : base(userService, cookieService)
        {
        }

        public override async Task Handle(UserChangedEmailEvent notification, CancellationToken cancellationToken)
        {
            await base.Handle(notification, cancellationToken);

            if (notification.User.EmailOnEmailChange == true)
            {
                // TODO: Send email
            }
        }
    }
}