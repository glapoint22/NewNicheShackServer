using MediatR;
using Microsoft.AspNetCore.Identity;

using Website.Application.Account.Common;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangeName.Commands
{
    public sealed class ChangeNameCommandHandler : UpdateUserCommandHandler, IRequestHandler<ChangeNameCommand, Result>
    {
        private readonly IUserService _userService;

        public ChangeNameCommandHandler(IUserService userService, ICookieService cookieService) : base(userService, cookieService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            if (user == null) throw new Exception("Error while trying to get user from claims.");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            user.AddDomainEvent(new UserChangedNameEvent(user.Id));

            IdentityResult result = await _userService.UpdateAsync(user);

            if (!result.Succeeded) return Result.Failed();


            //// Send the confirmation email
            //if (user.EmailOnNameChange == true)
            //{
            //    // TODO: Send email
            //}

            // Update the user cookie
            await UpdateUserCookie(user);

            return Result.Succeeded();
        }
    }
}