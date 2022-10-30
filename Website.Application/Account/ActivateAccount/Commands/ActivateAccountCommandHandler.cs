using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ActivateAccount.Commands
{
    public sealed class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, Result>
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

            if (user.EmailConfirmed) return Result.Failed("409");

            IdentityResult identityResult = await _userService.ConfirmEmailAsync(user, request.Token);
            if (!identityResult.Succeeded) return Result.Failed();

            // Add the domain event
            user.AddDomainEvent(new UserActivatedAccountEvent(user.Id));

            // Log in the user
            List<Claim> claims = _authService.GetClaims(user, true);
            string accessToken = _authService.GenerateAccessToken(claims);
            string refreshToken = _authService.GenerateRefreshToken(user.Id);
            string userData = _userService.GetUserData(user);
            DateTimeOffset? expiration = _userService.GetExpirationFromClaims(claims);

            // Set the cookies
            _cookieService.SetCookie("access", accessToken, expiration);
            _cookieService.SetCookie("refresh", refreshToken, expiration);
            _cookieService.SetCookie("user", userData, expiration);

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}