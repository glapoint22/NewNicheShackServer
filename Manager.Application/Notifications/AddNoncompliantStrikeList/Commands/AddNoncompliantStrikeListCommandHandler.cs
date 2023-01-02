using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.AddNoncompliantStrikeList.Commands
{
    public sealed class AddNoncompliantStrikeListCommandHandler : IRequestHandler<AddNoncompliantStrikeListCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public AddNoncompliantStrikeListCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddNoncompliantStrikeListCommand request, CancellationToken cancellationToken)
        {
            User? user = await _dbContext.Users
                .Where(x => x.Id == request.UserId)
                .SingleOrDefaultAsync();



            Notification notification = await _dbContext.Notifications
                 .Where(x => x.ListId == request.ListId && x.UserId == request.UserId && !x.IsArchived)
                 .Include(x => x.List)
                 .SingleAsync();



            if (notification.Name == notification.List.Name && notification.Text == notification.List.Description && user != null)
            {
                user.NoncompliantStrikes++;
                DomainEventsInterceptor.AddDomainEvent(new UserReceivedNoncompliantStrikeListEvent(user.Id, notification.List.Name, notification.List.Description));


                switch (request.Option)
                {
                    case 0:
                        notification.List.Name = "My List";
                        break;

                    case 1:
                        notification.List.Description = string.Empty;
                        break;

                    case 2:
                        notification.List.Name = "My List";
                        notification.List.Description = string.Empty;
                        break;
                }

                await _dbContext.SaveChangesAsync();
                return Result.Succeeded(true);
            }

            return Result.Succeeded();
        }
    }
}