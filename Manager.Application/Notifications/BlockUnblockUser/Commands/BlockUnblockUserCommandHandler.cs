using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.BlockUnblockUser.Commands
{
    public sealed class BlockUnblockUserCommandHandler : IRequestHandler<BlockUnblockUserCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public BlockUnblockUserCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Result> Handle(BlockUnblockUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _dbContext.Users.Where(x => x.Id == request.UserId).SingleAsync();

            user.BlockNotificationSending = !user.BlockNotificationSending;

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}