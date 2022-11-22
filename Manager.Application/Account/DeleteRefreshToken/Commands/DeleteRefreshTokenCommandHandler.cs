using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using System.Web;

namespace Manager.Application.Account.DeleteRefreshToken.Commands
{
    public sealed class DeleteRefreshTokenCommandHandler : IRequestHandler<DeleteRefreshTokenCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public DeleteRefreshTokenCommandHandler(IManagerDbContext dbContext, IAuthService authService)
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
