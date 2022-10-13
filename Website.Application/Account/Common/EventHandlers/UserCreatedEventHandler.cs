using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.Common.EventHandlers
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;
        private readonly ITaskService _taskService;

        public UserCreatedEventHandler(IUserService userService, IWebsiteDbContext dbContext, ITaskService taskService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _taskService = taskService;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            string key = "UserCreatedEventHandler-" + notification.User.Id;

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




            if (!notification.User.EmailConfirmed)
            {
                if (!_taskService.CompletedTasks.Contains(key))
                {
                    string token = await _userService.GenerateEmailConfirmationTokenAsync(notification.User);
                    // TODO: Send email

                    _taskService.CompletedTasks.Add(key);
                }
            }

            _taskService.CompletedTasks.Remove(key);
        }
    }
}