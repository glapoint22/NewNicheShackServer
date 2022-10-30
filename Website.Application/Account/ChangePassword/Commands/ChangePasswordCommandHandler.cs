using MediatR;
using Microsoft.AspNetCore.Identity;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangePassword.Commands
{
    public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IUserService _userService;

        public ChangePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            if (user == null) throw new Exception("Error while trying to get user from claims.");

            user.AddDomainEvent(new UserChangedPasswordEvent(user.Id));

            IdentityResult result = await _userService.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded) return Result.Failed();

            return Result.Succeeded();
        }
    }
}