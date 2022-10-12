using MediatR;
using Microsoft.AspNetCore.Identity;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.ChangeName.Commands
{
    public class ChangeNameCommandHandler : IRequestHandler<ChangeNameCommand, Result>
    {
        private IUserService _userService;
        private ICookieService _cookieService;

        public ChangeNameCommandHandler(IUserService userService, ICookieService cookieService)
        {
            _userService = userService;
            _cookieService = cookieService;
        }

        public async Task<Result> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            if (user != null)
            {
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;

                IdentityResult result = await _userService.UpdateAsync(user);

                if (result.Succeeded)
                {
                    if (user.EmailOnNameChange != null && user.EmailOnNameChange == true)
                    {
                        // TODO: Send email
                    }

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

            return Result.Failed();
        }
    }
}