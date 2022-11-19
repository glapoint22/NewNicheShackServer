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
        private readonly IWebsiteDbContext _dbContext;

        public ChangeEmailCommandHandler(IUserService userService, ICookieService cookieService, IWebsiteDbContext dbContext) : base(userService, cookieService)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            // Check password
            if (!await _userService.CheckPasswordAsync(user, request.Password)) return Result.Failed("401");

            // Change email
            IdentityResult result = await _userService.ChangeEmailAsync(user, request.NewEmail, request.OneTimePassword);
            if (!result.Succeeded) return Result.Failed("409");


            // Update the user cookie
            await UpdateUserCookie(user);

            user.AddDomainEvent(new UserChangedEmailEvent(user.Id));
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}