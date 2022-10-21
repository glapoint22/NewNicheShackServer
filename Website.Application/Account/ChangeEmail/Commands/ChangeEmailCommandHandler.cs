using MediatR;
using Microsoft.AspNetCore.Identity;
using Website.Application.Account.Common;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
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

            if (user == null) throw new Exception("Error while trying to get user from claims.");

            user.AddDomainEvent(new UserChangedEmailEvent(user.Id));

            IdentityResult result = await _userService.ChangeEmailAsync(user, request.NewEmail, request.OneTimePassword);

            if (!await _userService.CheckPasswordAsync(user, request.Password) || !result.Succeeded) return Result.Failed();

            //// Send the confirmation email
            //if (user.EmailOnEmailChange == true)
            //{
            //    // TODO: Send email
            //}

            // Update the user cookie
            await UpdateUserCookie(user);


            return Result.Succeeded();
        }
    }
}