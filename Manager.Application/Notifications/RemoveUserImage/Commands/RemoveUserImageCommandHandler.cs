using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.RemoveUserImage.Commands
{
    public sealed class RemoveUserImageCommandHandler : IRequestHandler<RemoveUserImageCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RemoveUserImageCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RemoveUserImageCommand request, CancellationToken cancellationToken)
        {
            User? user = await _dbContext.Users
                .Where(x => x.Id == request.UserId)
                .SingleOrDefaultAsync();

            if (user != null && user.Image == request.UserImage)
            {
                if (!user.Suspended)
                {
                    user.AddStrike();

                    DomainEventsInterceptor.AddDomainEvent(new UserReceivedNoncompliantStrikeUserImageEvent(user.FirstName, user.LastName, user.Email, user.Image));


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
                }



                user.RemoveImage();


                NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications
                    .Where(z => z.Id == request.NotificationId))
                .SingleOrDefaultAsync();

                notificationGroup?.ArchiveNotification();

                await _dbContext.SaveChangesAsync();


                return Result.Succeeded(true);
            }

            return Result.Succeeded();
        }
    }
}