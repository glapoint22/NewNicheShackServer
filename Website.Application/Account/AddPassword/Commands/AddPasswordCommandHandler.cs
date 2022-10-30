using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;

namespace Website.Application.Account.AddPassword.Commands
{
    public sealed class AddPasswordCommandHandler : IRequestHandler<AddPasswordCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;
        private readonly IWebsiteDbContext _dbContext;

        public AddPasswordCommandHandler(IUserService userService, IAuthService authService, ICookieService cookieService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _authService = authService;
            _cookieService = cookieService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddPasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            if (user != null)
            {
                IdentityResult result = new();
                bool hasPassword = await _userService.HasPasswordAsync(user);

                if (!hasPassword)
                {
                    result = await _userService.AddPasswordAsync(user, request.Password);
                }


                if (hasPassword || result.Succeeded)
                {
                    string? externalLogInProvider = _userService.GetExternalLogInProviderFromClaims();

                    if (externalLogInProvider == null) throw new Exception("Error while trying to get external log in provider from claims.");

                    // Log in the user
                    List<Claim> claims = _authService.GetClaims(user, externalLogInProvider, true);
                    string accessToken = _authService.GenerateAccessToken(claims);
                    string refreshToken = _authService.GenerateRefreshToken(user.Id);
                    string userData = _userService.GetUserData(user, externalLogInProvider, true);
                    DateTimeOffset? expiration = _userService.GetExpirationFromClaims(claims);

                    // Set the cookies
                    _cookieService.SetCookie("access", accessToken, expiration);
                    _cookieService.SetCookie("refresh", refreshToken, expiration);
                    _cookieService.SetCookie("user", userData, expiration);

                    await _dbContext.SaveChangesAsync();

                    return Result.Succeeded();
                }
            }

            throw new Exception("Error while trying to get user from claims.");
        }
    }
}
