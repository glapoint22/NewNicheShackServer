using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using System.Security.Claims;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.Refresh.Commands
{
    public sealed class RefreshCommandHandler : IRequestHandler<RefreshCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly ICookieService _cookieService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public RefreshCommandHandler(IUserService userService, IWebsiteDbContext dbContext, ICookieService cookieService, IAuthService authService, IConfiguration configuration)
        {
            _userService = userService;
            _dbContext = dbContext;
            _cookieService = cookieService;
            _authService = authService;
            _configuration = configuration;
        }

        public async Task<Result> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            RefreshToken newRefreshToken = null!;

            // Get the refresh token cookie
            string? refreshTokenCookie = _cookieService.GetCookie("refresh");

            if (refreshTokenCookie == null)
            {
                return Result.Failed("404");
            }



            // Get the access token
            string? accessToken = _authService.GetAccessTokenFromHeader();

            if (accessToken == null)
            {
                return Result.Failed("404");
            }



            // Get the claims principal from the token
            ClaimsPrincipal? principal = _authService.GetPrincipalFromToken(accessToken);

            if (principal == null)
            {
                return Result.Failed("404");
            }



            // Get the userId from the access token
            List<Claim> claims = principal.Claims.ToList();
            string? userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Result.Failed("404");
            }



            // Get the device cookie
            string? deviceCookie = _cookieService.GetCookie("device");

            if (deviceCookie == null)
            {
                return Result.Failed("404");
            }


            // Get the refresh token
            RefreshToken? refreshToken = await _dbContext.RefreshTokens
                .AsNoTracking()
                .Where(x => x.Id == refreshTokenCookie && x.UserId == userId && x.DeviceId == deviceCookie)
                .SingleOrDefaultAsync();

            if (refreshToken == null)
            {
                return Result.Failed("404");
            }



            // Check to see if the refresh token has expired
            if (DateTime.Compare(DateTime.UtcNow, refreshToken.Expiration) > 0)
            {
                return Result.Failed("404");
            }



            // Get the user
            User user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return Result.Failed("404");
            }


            // Get new access and refresh tokens
            accessToken = _authService.GenerateAccessToken(claims);
            newRefreshToken = RefreshToken.Create(user.Id, _configuration["TokenValidation:RefreshExpiresInDays"], deviceCookie);

            // Add the new refresh token to the database
            _dbContext.RefreshTokens.Add(newRefreshToken);

            // Create the user data
            string userData = user.FirstName + "," + user.LastName + "," + user.Email + "," + user.Image;

            // Set the expiration
            Claim? expirationClaim = principal.FindFirst(ClaimTypes.Expiration);
            DateTimeOffset? expiration = expirationClaim != null ? DateTimeOffset.Parse(expirationClaim.Value) : null;

            // Set the cookies
            _cookieService.SetCookie("access", accessToken, expiration);
            _cookieService.SetCookie("refresh", newRefreshToken.Id, expiration);
            _cookieService.SetCookie("user", userData, expiration);
            _cookieService.SetCookie("device", deviceCookie, expiration);

            // Save changes
            await _dbContext.SaveChangesAsync();


            // Return with the new refresh token ID
            return Result.Succeeded(new
            {
                value = newRefreshToken.Id
            });
        }
    }
}