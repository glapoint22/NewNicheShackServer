using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.ResetPassword.Commands
{
    public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly IUserService _userService;

        public ResetPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserByEmailAsync(request.Email);

            IdentityResult result = await _userService.ResetPasswordAsync(user, request.OneTimePassword, request.NewPassword);

            if (!result.Succeeded) return Result.Failed("409");

            return Result.Succeeded();
        }
    }
}