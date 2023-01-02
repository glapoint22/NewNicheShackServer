using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.ArchiveAll.Commands
{
    public sealed class ArchiveAllCommandHandler : IRequestHandler<ArchiveAllCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public ArchiveAllCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ArchiveAllCommand request, CancellationToken cancellationToken)
        {
            NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                .Where(x => x.Id == request.NotificationGroupId)
                .Include(x => x.Notifications)
                .SingleOrDefaultAsync();

            notificationGroup?.ArchiveAll();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}