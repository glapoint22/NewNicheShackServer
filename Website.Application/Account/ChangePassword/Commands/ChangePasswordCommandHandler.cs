using MediatR;
using Microsoft.AspNetCore.Identity;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.ChangePassword.Commands
{
    public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public ChangePasswordCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            IdentityResult result = await _userService.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded) return Result.Failed();

            user.AddDomainEvent(new UserChangedPasswordEvent(user.Id));
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}