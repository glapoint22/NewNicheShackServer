using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using System.Security.Claims;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.LogIn.Commands
{
    public sealed class LogInCommandHandler : IRequestHandler<LogInCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public LogInCommandHandler(IUserService userService, IAuthService authService, ICookieService cookieService, IWebsiteDbContext dbContext, IConfiguration configuration)
        {
            _userService = userService;
            _authService = authService;
            _cookieService = cookieService;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<Result> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByEmailAsync(request.Email.ToLower());

            if (user == null || await _userService.CheckPasswordAsync(user, request.Password) == false || user.Suspended)
            {
                return Result.Failed("401");
            }

            if (!user.EmailConfirmed) return Result.Failed("409");


            // Generate the claims and the tokens
            List<Claim> claims = _authService.GenerateClaims(user.Id, "user", request.IsPersistent);
            string accessToken = _authService.GenerateAccessToken(claims);
            
            string userData = _userService.GetUserData(user);
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

            // Add the refresh token to the database
            _dbContext.RefreshTokens.Add(refreshToken);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}