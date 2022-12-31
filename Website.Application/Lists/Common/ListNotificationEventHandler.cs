using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.Common
{
    public sealed class ListNotificationEventHandler : INotificationHandler<ListEvent>
    {
        private readonly IWebsiteDbContext _dbContext;

        public ListNotificationEventHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ListEvent notification, CancellationToken cancellationToken)
        {
            // Check to see if this user is blocked
            if (await _dbContext.Users.AnyAsync(x => x.Id == notification.UserId && x.BlockNotificationSending)) return;


            // First, check to see if a notification group for this type of notification already exists
            Guid notificationGroupId = await _dbContext.Notifications
                .Where(x => x.Type == (int)NotificationType.List && x.UserId == notification.UserId)
                .Select(x => x.NotificationGroupId)
                .FirstOrDefaultAsync();

            var list = await _dbContext.Lists
                .Where(x => x.Id == notification.ListId)
                .Select(x => new
                {
                    x.Name,
                    x.Description
                }).SingleAsync();


            // Create the notification
            Notification ListNotification = Notification.CreateListNotification(notificationGroupId, notification.ListId, notification.UserId, list.Name, list.Description);
            _dbContext.Notifications.Add(ListNotification);

            await _dbContext.SaveChangesAsync();
        }
    }
}