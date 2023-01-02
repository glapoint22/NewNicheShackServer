using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.Restore.Commands
{
    public sealed class RestoreCommandHandler : IRequestHandler<RestoreCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RestoreCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RestoreCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications)
                .SingleOrDefaultAsync();

            notificationGroup?.Restore(request.NotificationId);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}