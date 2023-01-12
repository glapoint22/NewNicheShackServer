using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.BlockNonAccountUser.Commands
{
    internal class BlockNonAccountUserCommandHandler : IRequestHandler<BlockNonAccountUserCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public BlockNonAccountUserCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(BlockNonAccountUserCommand request, CancellationToken cancellationToken)
        {
            var email = await _dbContext.BlockedNonAccountUsers.Where(x => x.Email == request.Email).FirstOrDefaultAsync();

            if (email == null)
            {
                BlockedNonAccountUser blockedNonAccountUser = new()
                {
                    Email = request.Email,
                    Name = request.UserName
                };
                _dbContext.BlockedNonAccountUsers.Add(blockedNonAccountUser);
                await _dbContext.SaveChangesAsync();
            }
            return Result.Succeeded();
        }
    }
}