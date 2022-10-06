using MediatR;
using Website.Domain.Events;

namespace Website.Application.Account.ActivateAccount.EventHandlers
{
    public class ActivateAccountCommandWelecomeEmailHandler : INotificationHandler<ActivateAccountSucceededEvent>
    {
        public Task Handle(ActivateAccountSucceededEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
