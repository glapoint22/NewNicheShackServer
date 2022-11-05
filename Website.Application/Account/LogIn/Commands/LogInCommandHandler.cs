﻿using MediatR;
using System.Security.Claims;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;

namespace Website.Application.Account.LogIn.Commands
{
    public sealed class LogInCommandHandler : IRequestHandler<LogInCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;
        private readonly IWebsiteDbContext _dbContext;

        public LogInCommandHandler(IUserService userService, IAuthService authService, ICookieService cookieService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _authService = authService;
            _cookieService = cookieService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByEmailAsync(request.Email);

            if (user == null || await _userService.CheckPasswordAsync(user, request.Password) == false)
            {
                return Result.Failed("401");
            }

            if (!user.EmailConfirmed) return Result.Failed("409");


            // Log in the user
            List<Claim> claims = _authService.GetClaims(user, request.IsPersistent);
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