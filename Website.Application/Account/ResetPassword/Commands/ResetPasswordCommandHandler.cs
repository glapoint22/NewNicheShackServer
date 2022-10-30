using MediatR;
using Microsoft.AspNetCore.Identity;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;

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

            if (user != null)
            {
                IdentityResult result = await _userService.ResetPasswordAsync(user, request.Token, request.NewPassword);

                if (result.Succeeded) return Result.Succeeded();

                return Result.Failed();
            }

            throw new Exception("Error while trying to get user from email.");
        }
    }
}