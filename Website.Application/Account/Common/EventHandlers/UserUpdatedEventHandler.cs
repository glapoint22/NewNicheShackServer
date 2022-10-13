using MediatR;
using Website.Application.Common.Interfaces;
using Website.Domain.Interfaces;

namespace Website.Application.Account.Common.EventHandlers
{
    public abstract class UserUpdatedEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IUserUpdatedEvent
    {
        private readonly IUserService _userService;
        private readonly ICookieService _cookieService;

        public UserUpdatedEventHandler(IUserService userService, ICookieService cookieService)
        {
            _userService = userService;
            _cookieService = cookieService;
        }

        public virtual async Task Handle(TEvent notification, CancellationToken cancellationToken)
        {
            // This will update the user cookie
            string userData;
            DateTimeOffset? expiration = _userService.GetExpirationFromClaims();
            string externalLogInProvider = _userService.GetExternalLogInProviderFromClaims();

            if (externalLogInProvider != null)
            {
                bool hasPassword = await _userService.HasPasswordAsync(notification.User);
                userData = _userService.GetUserData(notification.User, externalLogInProvider, hasPassword);
            }
            else
            {
                userData = _userService.GetUserData(notification.User);
            }

            _cookieService.SetCookie("user", userData, expiration);
        }
    }
}