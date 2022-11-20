using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using System.Security.Claims;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ActivateAccount.Commands
{
    public sealed class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;
        private readonly IConfiguration _configuration;

        public ActivateAccountCommandHandler(IUserService userService, IWebsiteDbContext dbContext, IAuthService authService, ICookieService cookieService, IConfiguration configuration)
        {
            _userService = userService;
            _dbContext = dbContext;
            _authService = authService;
            _cookieService = cookieService;
            _configuration = configuration;
        }

        public async Task<Result> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByEmailAsync(request.Email);

            if (user == null) throw new Exception();

            if (!user.EmailConfirmed)
            {
                IdentityResult identityResult = await _userService.ConfirmEmailAsync(user, request.OneTimePassword);
                if (!identityResult.Succeeded) return Result.Failed();

                // Log in the user
                List<Claim> claims = _authService.GenerateClaims(user.Id, "user", true);
                string accessToken = _authService.GenerateAccessToken(claims);
                RefreshToken refreshToken = RefreshToken.Create(user.Id, _configuration["TokenValidation:RefreshExpiresInDays"]);
                string userData = _userService.GetUserData(user);
                DateTimeOffset? expiration = _userService.GetExpirationFromClaims(claims);

                // Set the cookies
                _cookieService.SetCookie("access", accessToken, expiration);
                _cookieService.SetCookie("refresh", refreshToken.Id, expiration);
                _cookieService.SetCookie("user", userData, expiration);

                _dbContext.RefreshTokens.Add(refreshToken);
                await _dbContext.SaveChangesAsync();


                // Add the domain event
                user.AddDomainEvent(new UserActivatedAccountEvent(user.Id));
            }

            return Result.Succeeded();
        }
    }
}