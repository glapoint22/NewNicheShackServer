using MediatR;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.SignUp.EventHandlers
{
    public class UserCreatedEmailConfirmationEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IUserService _userService;

        public UserCreatedEmailConfirmationEventHandler(IUserService userService)
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