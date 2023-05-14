using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using System.Web;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.DeleteRefreshToken.Commands
{
    public sealed class DeleteRefreshTokenCommandHandler : IRequestHandler<DeleteRefreshTokenCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;

        public DeleteRefreshTokenCommandHandler(IWebsiteDbContext dbContext, IAuthService authService, ICookieService cookieService)
        {
            _dbContext = dbContext;
            _authService = authService;
            _cookieService = cookieService;
        }

        public async Task<Result> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();
            string? deviceCookie = _cookieService.GetCookie("device");

            // Get all refresh tokens from this user except the new refresh token
            List<RefreshToken> tokens = await _dbContext.RefreshTokens
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.Id != HttpUtility.UrlDecode(request.NewRefreshToken) && x.DeviceId == deviceCookie)
                .ToListAsync();

            // Delete all old refresh tokens
            _dbContext.RefreshTokens.RemoveRange(tokens);

            await _dbContext.SaveChangesAsync();


            return Result.Succeeded();
        }
    }
}
