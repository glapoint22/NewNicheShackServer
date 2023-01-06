using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.ArchiveGroup.Commands
{
    public sealed class ArchiveGroupCommandHandler : IRequestHandler<ArchiveGroupCommand, Result>
    {

        private readonly IWebsiteDbContext _dbContext;

        public ArchiveGroupCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ArchiveGroupCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
               .Where(x => x.Id == request.NotificationGroupId)
               .SingleOrDefaultAsync();

            notificationGroup?.ArchiveGroup();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}