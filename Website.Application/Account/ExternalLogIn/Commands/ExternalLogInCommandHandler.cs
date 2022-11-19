using MediatR;
using System.Security.Claims;
using Website.Application.Common.Classes;
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

        public ExternalLogInCommandHandler(IUserService userService, IWebsiteDbContext dbContext, IAuthService authService, ICookieService cookieService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _authService = authService;
            _cookieService = cookieService;
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
                List<Claim> claims = _authService.GetClaims(user, request.Provider, userHasPassword);
                string accessToken = _authService.GenerateAccessToken(claims);
                string refreshToken = _authService.GenerateRefreshToken(user.Id);
                string userData = _userService.GetUserData(user, request.Provider, userHasPassword);
                DateTimeOffset? expiration = _userService.GetExpirationFromClaims(claims);

                // Set the cookies
                _cookieService.SetCookie("access", accessToken, expiration);
                _cookieService.SetCookie("refresh", refreshToken, expiration);
                _cookieService.SetCookie("user", userData, expiration);

                await _dbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}