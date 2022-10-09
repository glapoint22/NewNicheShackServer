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
            IdentityResult identityResult = await _userService.ConfirmEmailAsync(user, request.Token);

            if (!identityResult.Succeeded) return Result.Failed();


            // Log in the user
            List<Claim> claims = _authService.GetClaims(user, true);
            string accessToken = _authService.GenerateAccessToken(claims);
            string refreshToken = await _authService.GenerateRefreshTokenAsync(user.Id);
            string userData = _userService.GetUserData(user);

            // Set the cookies
            _cookieService.SetCookie("access", accessToken, true);
            _cookieService.SetCookie("refresh", refreshToken, true);
            _cookieService.SetCookie("user", userData, true);

            // TODO: Add domain event for welcome email

            return Result.Succeeded();
        }
    }
}