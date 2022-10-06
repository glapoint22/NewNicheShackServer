using MediatR;
using Microsoft.AspNetCore.Identity;
using Website.Application.Account.ActivateAccount.Commands;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ActivateAccount.CommandHandlers
{
    public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly IPublisher _publisher;

        public ActivateAccountCommandHandler(IIdentityService identityService, IPublisher publisher)
        {
            _identityService = identityService;
            _publisher = publisher;
        }

        public async Task<Result> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            User user = await _identityService.GetUserByEmailAsync(request.Email);
            IdentityResult identityResult = await _identityService.ConfirmEmailAsync(user, request.Token);

            if (!identityResult.Succeeded) return Result.Failed();

            await _publisher.Publish(new ActivateAccountSucceededEvent(user), cancellationToken);

            await _identityService.LogInAsync(user, true);

            return Result.Succeeded();
        }
    }
}