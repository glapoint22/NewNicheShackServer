using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.Archive.Commands
{
    public sealed class ArchiveCommandHandler : IRequestHandler<ArchiveCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public ArchiveCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ArchiveCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications
                    .Where(z => z.Id == request.NotificationId))
                .SingleOrDefaultAsync();

            notificationGroup?.Archive();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}