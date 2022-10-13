using Website.Application.Account.Common.EventHandlers;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeName.EventHandlers
{
    public class UserChangedNameEventHandler : UserUpdatedEventHandler<UserChangedNameEvent>
    {
        public UserChangedNameEventHandler(IUserService userService, ICookieService cookieService) : base(userService, cookieService)
        {
        }

        public override async Task Handle(UserChangedNameEvent notification, CancellationToken cancellationToken)
        {
            await base.Handle(notification, cancellationToken);

            if (notification.User.EmailOnNameChange == true)
            {
                // TODO: Send email
            }
        }
    }
}