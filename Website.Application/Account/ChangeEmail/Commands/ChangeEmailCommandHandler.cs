using MediatR;
using Microsoft.AspNetCore.Identity;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.ChangeEmail.Commands
{
    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly ICookieService _cookieService;

        public ChangeEmailCommandHandler(IUserService userService, ICookieService cookieService)
        {
            _userService = userService;
            _cookieService = cookieService;
        }

        public async Task<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            if (user != null)
            {
                if (await _userService.CheckPasswordAsync(user, request.Password))
                {
                    IdentityResult result = await _userService.ChangeEmailAsync(user, request.NewEmail, request.OneTimePassword);

                    if (result.Succeeded)
                    {
                        string userData;
                        DateTimeOffset? expiration = _userService.GetExpirationFromClaims();
                        string externalLogInProvider = _userService.GetExternalLogInProviderFromClaims();

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

                        return Result.Succeeded();
                    }
                }
            }

            return Result.Failed();
        }
    }
}