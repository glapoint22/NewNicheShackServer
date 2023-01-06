using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.ArchiveAllNotifications.Commands
{
    public sealed class ArchiveAllNotificationsCommandHandler : IRequestHandler<ArchiveAllNotificationsCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public ArchiveAllNotificationsCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ArchiveAllNotificationsCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications)
                .SingleOrDefaultAsync();

            notificationGroup?.ArchiveAllNotifications();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}