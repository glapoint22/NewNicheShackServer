using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Entities;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;
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
                .Where(x => x.Type == (int)NotificationType.List && x.UserId == notification.UserId && x.ListId == notification.ListId)
                .Select(x => x.NotificationGroupId)
                .FirstOrDefaultAsync();


            // Create the notification
            Notification ListNotification = Notification.CreateListNotification(notificationGroupId, notification.ListId, notification.Name, notification.Description, notification.UserId);
            _dbContext.Notifications.Add(ListNotification);

            await _dbContext.SaveChangesAsync();
        }
    }
}