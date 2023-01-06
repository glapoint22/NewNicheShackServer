using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.RestoreGroup.Commands
{
    public sealed class RestoreGroupCommandHandler : IRequestHandler<RestoreGroupCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RestoreGroupCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RestoreGroupCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
               .Where(x => x.Id == request.NotificationGroupId)
               .SingleOrDefaultAsync();

            notificationGroup?.RestoreGroup();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}