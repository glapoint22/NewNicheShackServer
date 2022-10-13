using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.ActivateAccount.Commands
{
    public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;

        public ActivateAccountCommandHandler(IUserService userService, IWebsiteDbContext dbContext, IAuthService authService, ICookieService cookieService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _authService = authService;
            _cookieService = cookieService;
        }

        public async Task<Result> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByEmailAsync(request.Email);

            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    IdentityResult identityResult = await _userService.ConfirmEmailAsync(user, request.Token);
                    if (!identityResult.Succeeded) return Result.Failed();

                    // TODO: Send welcome email
                }

                // Log in the user
                List<Claim> claims = _authService.GetClaims(user, true);
                string accessToken = _authService.GenerateAccessToken(claims);
                string refreshToken = _authService.GenerateRefreshToken(user.Id);
                string userData = _userService.GetUserData(user);
                DateTimeOffset expiration = _userService.GetExpirationFromClaims(claims);

                // Set the cookies
                _cookieService.SetCookie("access", accessToken, expiration);
                _cookieService.SetCookie("refresh", refreshToken, expiration);
                _cookieService.SetCookie("user", userData, expiration);

                await _dbContext.SaveChangesAsync();

                return Result.Succeeded();
            }

            return Result.Failed();
        }
    }
}