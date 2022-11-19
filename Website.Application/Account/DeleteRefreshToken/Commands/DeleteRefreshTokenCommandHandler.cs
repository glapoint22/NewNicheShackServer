using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Web;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.DeleteRefreshToken.Commands
{
    public sealed class DeleteRefreshTokenCommandHandler : IRequestHandler<DeleteRefreshTokenCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public DeleteRefreshTokenCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

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
