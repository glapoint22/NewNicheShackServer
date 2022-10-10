using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.AddPassword.Commands
{
    public class AddPasswordCommandHandler : IRequestHandler<AddPasswordCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;

        public AddPasswordCommandHandler(IUserService userService, IAuthService authService, ICookieService cookieService)
        {
            _userService = userService;
            _authService = authService;
            _cookieService = cookieService;
        }

        public async Task<Result> Handle(AddPasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            IdentityResult result = await _userService.AddPasswordAsync(user, request.Password);

            if (result.Succeeded)
            {
                string externalLogInProvider = _userService.GetExternalLogInProviderFromClaims();

                // Log in the user
                List<Claim> claims = _authService.GetClaims(user, externalLogInProvider, true);
                string accessToken = _authService.GenerateAccessToken(claims);
                string refreshToken = await _authService.GenerateRefreshTokenAsync(user.Id);
                string userData = _userService.GetUserData(user, externalLogInProvider, true);

                // Set the cookies
                _cookieService.SetCookie("access", accessToken, true);
                _cookieService.SetCookie("refresh", refreshToken, true);
                _cookieService.SetCookie("user", userData, true);

                return Result.Succeeded();
            }

            return Result.Failed();
        }
    }
}
