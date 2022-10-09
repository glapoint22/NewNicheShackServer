using MediatR;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.Common.EventHandlers
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public UserCreatedEventHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            string token = await _userService.GenerateEmailConfirmationTokenAsync(notification.User);

            // Create the user's first list
            List list = new()
            {
                Id = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(),
                Name = notification.User.FirstName + "'s List",
                Description = string.Empty,
                CollaborateId = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper()
            };

            Collaborator collaborator = new(list.Id, notification.User.Id, true);

            _dbContext.Lists.Add(list);
            _dbContext.Collaborators.Add(collaborator);
        }
    }
}