using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.RestoreAllNotifications.Commands
{
    public sealed class RestoreAllNotificationsCommandHandler : IRequestHandler<RestoreAllNotificationsCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RestoreAllNotificationsCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RestoreAllNotificationsCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications)
                .SingleOrDefaultAsync();

            notificationGroup?.RestoreAllNotifications();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}