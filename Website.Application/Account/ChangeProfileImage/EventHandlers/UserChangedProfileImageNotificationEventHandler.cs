using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeProfileImage.EventHandlers
{
    public sealed class UserChangedProfileImageNotificationEventHandler : INotificationHandler<UserChangedProfileImageEvent>
    {
        private readonly IWebsiteDbContext _dbContext;

        public UserChangedProfileImageNotificationEventHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UserChangedProfileImageEvent notification, CancellationToken cancellationToken)
        {
            User user = (await _dbContext.Users.FindAsync(notification.UserId))!;

            if (user.BlockNotificationSending) return;


            // First, check to see if a notification group for this type of notification already exists
            Guid notificationGroupId = await _dbContext.Notifications
                .Where(x => x.Type == (int)NotificationType.UserImage && x.UserId == user.Id)
                .Select(x => x.NotificationGroupId)
                .FirstOrDefaultAsync();


            // Create the notification
            Notification newUserImageNotification = Notification.CreateNewUserImageNotification(notificationGroupId, user.Id, user.Image!);
            _dbContext.Notifications.Add(newUserImageNotification);

            await _dbContext.SaveChangesAsync();
        }
    }
}