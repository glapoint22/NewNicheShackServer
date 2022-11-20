using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Lists.DeleteList.Commands
{
    public sealed class DeleteListCommandHandler : IRequestHandler<DeleteListCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public DeleteListCommandHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(DeleteListCommand request, CancellationToken cancellationToken)
        {
            string userId = _userService.GetUserIdFromClaims();

            // Get the list to delete
            List? list = await _dbContext.Lists
                .Where(x => x.Id == request.Id && x.Collaborators
                    .Any(z => z.UserId == userId && (z.IsOwner || z.CanDeleteList)))
                .Include(x => x.Collaborators
                    .Where(x => x.UserId != userId && x.User.EmailOnDeletedList == true))
                .SingleOrDefaultAsync();

            if (list == null) return Result.Failed();

            List<Notification> notifications = await _dbContext.Notifications
                .Where(x => x.ListId == list.Id)
                .Include(x => x.NotificationGroup)
                .ToListAsync();

            if (notifications.Count > 0)
            {
                _dbContext.Notifications.RemoveRange(notifications);
                _dbContext.NotificationGroups.Remove(notifications.Select(x => x.NotificationGroup).First());
            }


            // Add the list deleted event
            if (list.Collaborators.Count > 0)
            {
                list.AddDomainEvent(new ListDeletedEvent(list.Name, userId, list.Collaborators.Select(x => x.UserId).ToList()));
            }

            // Delete and save
            _dbContext.Lists.Remove(list);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}