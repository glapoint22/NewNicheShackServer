using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.ArchiveNotification.Commands
{
    public sealed class ArchiveNotificationCommandHandler : IRequestHandler<ArchiveNotificationCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public ArchiveNotificationCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ArchiveNotificationCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications
                    .Where(z => z.Id == request.NotificationId))
                .SingleOrDefaultAsync();

            notificationGroup?.ArchiveNotification();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}