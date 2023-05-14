using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using System.Security.Claims;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.ExternalLogIn.Commands
{
    public sealed class ExternalLogInCommandHandler : IRequestHandler<ExternalLogInCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;
        private readonly IConfiguration _configuration;

        public ExternalLogInCommandHandler(IUserService userService, IWebsiteDbContext dbContext, IAuthService authService, ICookieService cookieService, IConfiguration configuration)
        {
            _userService = userService;
            _dbContext = dbContext;
            _authService = authService;
            _cookieService = cookieService;
            _configuration = configuration;
        }

        public async Task<Result> Handle(ExternalLogInCommand request, CancellationToken cancellationToken)
        {
            User? user;

            user = await _userService.GetUserByEmailAsync(request.Email);

            user ??= await _userService.CreateUserAsync(request.FirstName, request.LastName, request.Email);

            // Log in the user
            if (user != null)
            {
                bool userHasPassword = await _userService.HasPasswordAsync(user);
                List<Claim> claims = _authService.GenerateClaims(user.Id, request.Provider, userHasPassword);
                string accessToken = _authService.GenerateAccessToken(claims);
                
                string userData = _userService.GetUserData(user, request.Provider, userHasPassword);
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
            }

            return Result.Succeeded();
        }
    }
}