using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.RestoreAll.Commands
{
    public sealed class RestoreAllCommandHandler : IRequestHandler<RestoreAllCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RestoreAllCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RestoreAllCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications)
                .SingleOrDefaultAsync();

            notificationGroup?.RestoreAll();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}