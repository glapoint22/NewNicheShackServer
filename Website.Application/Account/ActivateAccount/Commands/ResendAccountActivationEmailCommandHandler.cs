using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.ActivateAccount.Commands
{
    internal class ResendAccountActivationEmailCommandHandler : IRequestHandler<ResendAccountActivationEmailCommand, Result>
    {
        private readonly IUserService _userService;

        public ResendAccountActivationEmailCommandHandler(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<Result> Handle(ResendAccountActivationEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByEmailAsync(request.Email);

            // TODO: Send email

            return Result.Succeeded();
        }
    }
}