using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;

namespace Website.Application.Account.CreateChangeEmailOTP.Commands
{
    public sealed class CreateChangeEmailOTPCommandHandler : IRequestHandler<CreateChangeEmailOTPCommand, Result>
    {
        private readonly IUserService _userService;

        public CreateChangeEmailOTPCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(CreateChangeEmailOTPCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();
            string token = await _userService.GenerateChangeEmailTokenAsync(user, request.Email);

            // TODO: Send email

            return Result.Succeeded();
        }
    }
}