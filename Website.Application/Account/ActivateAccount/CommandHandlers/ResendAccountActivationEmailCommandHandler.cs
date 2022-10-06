using MediatR;
using Website.Application.Account.ActivateAccount.Commands;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ActivateAccount.CommandHandlers
{
    internal class ResendAccountActivationEmailCommandHandler : IRequestHandler<ResendAccountActivationEmailCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly IPublisher _publisher;

        public ResendAccountActivationEmailCommandHandler(IIdentityService identityService, IPublisher publisher)
        {
            _identityService = identityService;
            _publisher = publisher;
        }


        public async Task<Result> Handle(ResendAccountActivationEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _identityService.GetUserByEmailAsync(request.Email);
            await _publisher.Publish(new NewAccountEvent(user), cancellationToken);

            return Result.Succeeded();
        }
    }
}