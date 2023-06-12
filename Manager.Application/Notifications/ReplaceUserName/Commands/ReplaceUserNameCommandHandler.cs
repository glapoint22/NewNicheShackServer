using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using User = Website.Domain.Entities.User;

namespace Manager.Application.Notifications.ReplaceUserName.Commands
{
    public sealed class ReplaceUserNameCommandHandler : IRequestHandler<ReplaceUserNameCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public ReplaceUserNameCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ReplaceUserNameCommand request, CancellationToken cancellationToken)
        {
            User? user = await _dbContext.Users
                .Where(x => x.Id == request.UserId)
                .SingleOrDefaultAsync();

            if (user != null && user.FirstName + " " + user.LastName == request.UserName)
            {
                DomainEventsInterceptor.AddDomainEvent(new UserReceivedNoncompliantStrikeUserNameEvent(user.FirstName, user.LastName, user.Email));
                
                user.AddStrike();
                user.ChangeName("NicheShack", "User");


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