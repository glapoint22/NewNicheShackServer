using MediatR;
using Shared.Common.Entities;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Account.SignUp.EventHandlers
{
    public sealed class UserCreatedNotificationEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IUserService _userService;

        public UserCreatedNotificationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByIdAsync(notification.UserId);
            string token = await _userService.GenerateEmailConfirmationTokenAsync(user);
        }
    }
}