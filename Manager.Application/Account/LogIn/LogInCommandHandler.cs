using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared.Common.Classes;
using System.Security.Claims;

namespace Manager.Application.Account.LogIn
{
    public sealed class LogInCommandHandler : IRequestHandler<LogInCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;
        private readonly IManagerDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public LogInCommandHandler(UserManager<User> userManager, IAuthService authService, ICookieService cookieService, IManagerDbContext dbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _authService = authService;
            _cookieService = cookieService;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<Result> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || await _userManager.CheckPasswordAsync(user, request.Password) == false)
            {
                return Result.Failed("401");
            }

            // Generate the claims and the tokens
            List<Claim> claims = _authService.GenerateClaims(user.Id, "admin", true);
            string accessToken = _authService.GenerateAccessToken(claims);
            RefreshToken refreshToken = RefreshToken.Create(user.Id, _configuration["TokenValidation:RefreshExpiresInDays"]);
            string userData = user.FirstName + "," + user.LastName + "," + user.Email + "," + user.Image;
            DateTimeOffset? expiration = _authService.GetExpirationFromClaims(claims);

            // Set the cookies
            _cookieService.SetCookie("access", accessToken, expiration);
            _cookieService.SetCookie("refresh", refreshToken.Id, expiration);
            _cookieService.SetCookie("user", userData, expiration);

            _dbContext.RefreshTokens.Add(refreshToken);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}