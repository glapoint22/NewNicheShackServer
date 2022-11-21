using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.Common
{
    public class UpdateUserCommandHandler
    {
        private readonly IUserService _userService;
        private readonly ICookieService _cookieService;
        private readonly IAuthService _authService;

        public UpdateUserCommandHandler(IUserService userService, ICookieService cookieService, IAuthService authService)
        {
            _userService = userService;
            _cookieService = cookieService;
            _authService = authService;
        }

        public async Task UpdateUserCookie(User user)
        {
            // This will update the user cookie
            string userData;
            DateTimeOffset? expiration = _authService.GetExpirationFromClaims();
            string? externalLogInProvider = _authService.GetExternalLogInProviderFromClaims();

            if (externalLogInProvider != null)
            {
                bool hasPassword = await _userService.HasPasswordAsync(user);
                userData = _userService.GetUserData(user, externalLogInProvider, hasPassword);
            }
            else
            {
                userData = _userService.GetUserData(user);
            }

            _cookieService.SetCookie("user", userData, expiration);
        }
    }
}