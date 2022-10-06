using MediatR;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.Account.Common.EventHandlers
{
    public class NewAccountEventHandler : INotificationHandler<NewAccountEvent>
    {
        private readonly IIdentityService _identityService;

        public NewAccountEventHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task Handle(NewAccountEvent notification, CancellationToken cancellationToken)
        {
            string token = await _identityService.GenerateEmailConfirmationTokenAsync(notification.User);
        }
    }
}