using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.ReformList.Commands
{
    public sealed class ReformListCommandHandler : IRequestHandler<ReformListCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public ReformListCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ReformListCommand request, CancellationToken cancellationToken)
        {
            User? user = await _dbContext.Users
                .Where(x => x.Id == request.UserId)
                .SingleOrDefaultAsync();



            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications
                    .Where(z => z.Id == request.NotificationId))
                .ThenInclude(x => x.List)
                .SingleOrDefaultAsync();

            if (notificationGroup != null)
            {
                Notification notification = notificationGroup.Notifications[0];

                if (notification.Name == notification.List.Name && notification.Text == notification.List.Description && user != null)
                {
                    user.AddStrike();

                    DomainEventsInterceptor.AddDomainEvent(
                            new UserReceivedNoncompliantStrikeListEvent(
                                user.FirstName,
                                user.LastName,
                                user.Email,
                                notification.ListId!,
                                notification.List.Name,
                                notification.List.Description));


                    if (user.NoncompliantStrikes >= 3)
                    {
                        // Terminate account
                        user.Suspended = true;
                        DomainEventsInterceptor.AddDomainEvent(
                            new UserAccountTerminatedEvent(
                                user.FirstName,
                                user.LastName,
                                user.Email));
                    }


                    notification.List.ReformList(request.Option);
                    notificationGroup.ArchiveNotification();
                    await _dbContext.SaveChangesAsync();

                    return Result.Succeeded(true);
                }
            }

            return Result.Succeeded();
        }
    }
}