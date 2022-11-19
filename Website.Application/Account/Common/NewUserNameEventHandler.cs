using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.Common
{
    public sealed class NewUserNameEventHandler : INotificationHandler<UserEvent>
    {
        private readonly IWebsiteDbContext _dbContext;

        public NewUserNameEventHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UserEvent notification, CancellationToken cancellationToken)
        {
            User user = (await _dbContext.Users.FindAsync(notification.UserId))!;

            if (user.BlockNotificationSending) return;


            // First, check to see if a notification group for this type of notification already exists
            Guid notificationGroupId = await _dbContext.Notifications
                .Where(x => x.Type == (int)NotificationType.UserName && x.UserId == user.Id)
                .Select(x => x.NotificationGroupId)
                .FirstOrDefaultAsync();


            // Create the notification
            Notification newUserNameNotification = Notification.CreateNewUserNameNotification(notificationGroupId, user.Id, user.FirstName + ' ' + user.LastName);
            _dbContext.Notifications.Add(newUserNameNotification);

            await _dbContext.SaveChangesAsync();
        }
    }
}