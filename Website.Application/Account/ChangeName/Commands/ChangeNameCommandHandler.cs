using MediatR;
using Microsoft.AspNetCore.Identity;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.ChangeName.Commands
{
    public class ChangeNameCommandHandler : IRequestHandler<ChangeNameCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public ChangeNameCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            if (user != null)
            {
                user.ChangeName(request.FirstName, request.LastName);

                IdentityResult result = await _userService.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await _dbContext.SaveChangesAsync();
                    return Result.Succeeded();
                }
            }

            throw new Exception("Error while trying to get user from claims.");
        }
    }
}