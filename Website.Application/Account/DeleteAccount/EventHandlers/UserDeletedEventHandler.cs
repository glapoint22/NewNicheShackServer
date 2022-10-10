using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.DeleteAccount.EventHandlers
{
    internal class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
    {
        private readonly IWebsiteDbContext _dbContext;

        public UserDeletedEventHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            List<List> lists = await _dbContext.Collaborators
                .Where(x => x.UserId == notification.User.Id && x.IsOwner)
                .Select(x => x.List)
                .ToListAsync();

            _dbContext.Lists.RemoveRange(lists);

            // TODO: Send account deleted email
        }
    }
}