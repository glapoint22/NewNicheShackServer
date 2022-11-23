using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.DeleteNotifications.Commands
{
    public sealed class DeleteNotificationsCommandHandler : IRequestHandler<DeleteNotificationsCommand, Result>
    {
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IManagerDbContext _managerDbContext;

        public DeleteNotificationsCommandHandler(IWebsiteDbContext websiteDbContext, IManagerDbContext managerDbContext)
        {
            _websiteDbContext = websiteDbContext;
            _managerDbContext = managerDbContext;
        }

        public async Task<Result> Handle(DeleteNotificationsCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _websiteDbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications)
                .SingleOrDefaultAsync();

            if (notificationGroup != null)
            {
                if (request.NotificationIds.Count > 0 && request.NotificationIds.Count != notificationGroup.Notifications.Count)
                {
                    notificationGroup.DeleteNotifications(request.NotificationIds);
                }
                else
                {
                    _websiteDbContext.NotificationGroups.Remove(notificationGroup);
                }
            }

            await _websiteDbContext.SaveChangesAsync();


            var employeeNotes = await _managerDbContext.NotificationEmployeeNotes
                .Where(x => request.NotificationIds.Contains((Guid)x.NotificationId!))
                .ToListAsync();

            _managerDbContext.NotificationEmployeeNotes.RemoveRange(employeeNotes);

            await _managerDbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}