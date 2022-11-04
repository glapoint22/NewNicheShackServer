using MediatR;
using Microsoft.AspNetCore.Identity;

using Website.Application.Account.Common;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeEmail.Commands
{
    public sealed class ChangeEmailCommandHandler : UpdateUserCommandHandler, IRequestHandler<ChangeEmailCommand, Result>
    {
        private readonly IUserService _userService;

        public ChangeEmailCommandHandler(IUserService userService, ICookieService cookieService) : base(userService, cookieService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();
            IdentityResult result = await _userService.ChangeEmailAsync(user, request.NewEmail, request.OneTimePassword);

            if (!await _userService.CheckPasswordAsync(user, request.Password) || !result.Succeeded) return Result.Failed();

            // Update the user cookie
            await UpdateUserCookie(user);

            user.AddDomainEvent(new UserChangedEmailEvent(user.Id));


            return Result.Succeeded();
        }
    }
}