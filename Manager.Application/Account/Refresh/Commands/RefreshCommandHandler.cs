using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using System.Security.Claims;

namespace Manager.Application.Account.Refresh.Commands
{
    public sealed class RefreshCommandHandler : IRequestHandler<RefreshCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly ICookieService _cookieService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public RefreshCommandHandler(IManagerDbContext dbContext, ICookieService cookieService, IAuthService authService, IConfiguration configuration, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _cookieService = cookieService;
            _authService = authService;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<Result> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            RefreshToken newRefreshToken = null!;
            string? refreshTokenCookie = _cookieService.GetCookie("refresh");

            if (refreshTokenCookie != null)
            {
                string? accessToken = _authService.GetAccessTokenFromHeader();

                if (accessToken != null)
                {
                    ClaimsPrincipal? principal = _authService.GetPrincipalFromToken(accessToken);

                    if (principal != null)
                    {
                        List<Claim> claims = principal.Claims.ToList();

                        string? userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                        if (userId != null)
                        {
                            RefreshToken? refreshToken = await _dbContext.RefreshTokens
                                .AsNoTracking()
                                .Where(x => x.Id == refreshTokenCookie && x.UserId == userId)
                                .SingleOrDefaultAsync();

                            if (refreshToken != null)
                            {
                                if (DateTime.Compare(DateTime.UtcNow, refreshToken.Expiration) < 0)
                                {
                                    User user = await _userManager.FindByIdAsync(userId);

                                    if (user != null)
                                    {
                                        accessToken = _authService.GenerateAccessToken(claims);
                                        newRefreshToken = RefreshToken.Create(user.Id, _configuration["TokenValidation:RefreshExpiresInDays"]);

                                        _dbContext.RefreshTokens.Add(newRefreshToken);

                                        string userData = user.FirstName + "," + user.LastName + "," + user.Email + "," + user.Image;

                                        Claim? expirationClaim = principal.FindFirst(ClaimTypes.Expiration);
                                        DateTimeOffset? expiration = expirationClaim != null ? DateTimeOffset.Parse(expirationClaim.Value) : null;

                                        _cookieService.SetCookie("access", accessToken, expiration);
                                        _cookieService.SetCookie("refresh", newRefreshToken.Id, expiration);
                                        _cookieService.SetCookie("user", userData, expiration);

                                        await _dbContext.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (newRefreshToken != null)
            {
                return Result.Succeeded(new
                {
                    value = newRefreshToken.Id
                });
            }

            return Result.Succeeded();
        }
    }
}