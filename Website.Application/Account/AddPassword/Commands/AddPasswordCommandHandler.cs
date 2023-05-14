using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using System.Security.Claims;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.AddPassword.Commands
{
    public sealed class AddPasswordCommandHandler : IRequestHandler<AddPasswordCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AddPasswordCommandHandler(IUserService userService, IAuthService authService, ICookieService cookieService, IWebsiteDbContext dbContext, IConfiguration configuration)
        {
            _userService = userService;
            _authService = authService;
            _cookieService = cookieService;
            _dbContext = dbContext;
            _configuration = configuration;
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
                    string? externalLogInProvider = _authService.GetExternalLogInProviderFromClaims();

                    if (externalLogInProvider == null) throw new Exception("Error while trying to get external log in provider from claims.");

                    // Log in the user
                    List<Claim> claims = _authService.GenerateClaims(user.Id, externalLogInProvider, true);
                    string accessToken = _authService.GenerateAccessToken(claims);
                    
                    string userData = _userService.GetUserData(user, externalLogInProvider, true);
                    DateTimeOffset? expiration = _authService.GetExpirationFromClaims(claims);

                    // Set the device cookie
                    string? deviceCookie = _cookieService.GetCookie("device");
                    string deviceId = deviceCookie ?? Guid.NewGuid().ToString();

                    _cookieService.SetCookie("device", deviceId, expiration);

                    // Create the new refresh token
                    RefreshToken refreshToken = RefreshToken.Create(user.Id, _configuration["TokenValidation:RefreshExpiresInDays"], deviceId);


                    // Set the cookies
                    _cookieService.SetCookie("access", accessToken, expiration);
                    _cookieService.SetCookie("refresh", refreshToken.Id, expiration);
                    _cookieService.SetCookie("user", userData, expiration);

                    _dbContext.RefreshTokens.Add(refreshToken);
                    await _dbContext.SaveChangesAsync();

                    return Result.Succeeded();
                }
            }

            throw new Exception("Error while trying to get user from claims.");
        }
    }
}
