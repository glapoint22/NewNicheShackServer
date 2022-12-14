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

        public DeleteRefreshTokenCommandHandler(IWebsiteDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string userId = _authService.GetUserIdFromClaims();

            // Get all refresh tokens from this user except the new refresh token
            List<RefreshToken> tokens = await _dbContext.RefreshTokens
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.Id != HttpUtility.UrlDecode(request.NewRefreshToken))
                .ToListAsync();

            // Delete all old refresh tokens
            _dbContext.RefreshTokens.RemoveRange(tokens);

            await _dbContext.SaveChangesAsync();


            return Result.Succeeded();
        }
    }
}
