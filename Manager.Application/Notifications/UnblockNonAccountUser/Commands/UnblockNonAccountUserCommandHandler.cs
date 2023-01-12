using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.UnblockNonAccountUser.Commands
{
    public sealed class UnblockNonAccountUserCommandHandler : IRequestHandler<UnblockNonAccountUserCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public UnblockNonAccountUserCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UnblockNonAccountUserCommand request, CancellationToken cancellationToken)
        {
            BlockedNonAccountUser? blockedNonAccountUser = await _dbContext.BlockedNonAccountUsers.Where(x => x.Email == request.BlockedUserEmail).FirstOrDefaultAsync();

            if (blockedNonAccountUser != null)
            {
                _dbContext.BlockedNonAccountUsers.Remove(blockedNonAccountUser);
                await _dbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}
