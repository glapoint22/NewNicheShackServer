using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Enums;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using IAuthService = Manager.Application.Common.Interfaces.IAuthService;

namespace Manager.Application.Notifications.CreateNotification.Commands
{
    public sealed class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IAuthService _authService;

        public CreateNotificationCommandHandler(IManagerDbContext managerDbContext, IWebsiteDbContext websiteDbContext, IAuthService authService)
        {
            _managerDbContext = managerDbContext;
            _websiteDbContext = websiteDbContext;
            _authService = authService;
        }

        public async Task<Result> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            // First, check to see if a notification group for the type of notification that we're going to create already exists
            var notificationGroup = await _websiteDbContext.Notifications.Where(x => x.UserId == request.UserId.ToString() && x.Type == request.Type).Select(x => x.NotificationGroup).FirstOrDefaultAsync();

            // If a notification group does (NOT) exists
            if (notificationGroup == null)
            {
                // Create a new notification group
                notificationGroup = new NotificationGroup { };
                _websiteDbContext.NotificationGroups.Add(notificationGroup);
                await _websiteDbContext.SaveChangesAsync();
            }

            // Now create the new notification
            var notification = new Notification()
            {
                NotificationGroupId = notificationGroup.Id,
                UserId = request.UserId.ToString(),
                Type = request.Type,
                Name = request.UserName,
                UserImage = request.UserImage,
                CreationDate = DateTime.UtcNow
            };
            _websiteDbContext.Notifications.Add(notification);
            await _websiteDbContext.SaveChangesAsync();


            // If notes were written
            if (request.EmployeeNotes != null)
            {
                // Create the notes
                NotificationEmployeeNote notificationEmployeeNote = new NotificationEmployeeNote
                {
                    NotificationGroupId = notification.NotificationGroupId,
                    NotificationId = notification.Id,
                    EmployeeId = _authService.GetUserIdFromClaims(),
                    Note = request.EmployeeNotes,
                    CreationDate = DateTime.UtcNow
                };
                _managerDbContext.NotificationEmployeeNotes.Add(notificationEmployeeNote);
                await _managerDbContext.SaveChangesAsync();
            }


            // Return the notification item
            var NotificationItem = new
            {
                notification.Id,
                notification.NotificationGroupId,
                NotificationType = notification.Type,
                UserName = notification.Name,
                notification.UserImage,
                IsNew = true, //Needs to be set as new
                notification.CreationDate,
                Name = request.Type == (int)NotificationType.UserName ? "User Name" : "User Image"
            };

            return Result.Succeeded(NotificationItem);
        }
    }
}