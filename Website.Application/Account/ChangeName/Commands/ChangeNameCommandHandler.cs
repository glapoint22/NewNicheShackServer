using MediatR;
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

            // Change the name
            user.ChangeName(request.FirstName, request.LastName);
            user.AddDomainEvent(new UserChangedNameEvent(user.Id));

            await _userService.UpdateAsync(user);


            // Update the user cookie
            await UpdateUserCookie(user);

            return Result.Succeeded();
        }
    }
}