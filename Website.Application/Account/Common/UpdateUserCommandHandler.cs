
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;

namespace Website.Application.Account.Common
{
    public class UpdateUserCommandHandler
    {
        private readonly IUserService _userService;
        private readonly ICookieService _cookieService;

        public UpdateUserCommandHandler(IUserService userService, ICookieService cookieService)
        {
            _userService = userService;
            _cookieService = cookieService;
        }

        public async Task UpdateUserCookie(User user)
        {
            // This will update the user cookie
            string userData;
            DateTimeOffset? expiration = _userService.GetExpirationFromClaims();
            string? externalLogInProvider = _userService.GetExternalLogInProviderFromClaims();

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