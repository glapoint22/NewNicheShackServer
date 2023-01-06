using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.RestoreNotification.Commands
{
    public sealed class RestoreNotificationCommandHandler : IRequestHandler<RestoreNotificationCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RestoreNotificationCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RestoreNotificationCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications)
                .SingleOrDefaultAsync();

            notificationGroup?.RestoreNotification(request.NotificationId);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}