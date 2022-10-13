using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.DeleteAccount.Commands
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public DeleteAccountCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetUserFromClaimsAsync();

            if (user != null)
            {
                if (await _userService.VerifyDeleteAccountTokenAsync(user, request.Token) && await _userService.CheckPasswordAsync(user, request.Password))
                {
                    _dbContext.Users.Remove(user);
                    user.AddDomainEvent(new UserDeletedEvent(user));

                    await _dbContext.SaveChangesAsync();

                    return Result.Succeeded();
                }

                return Result.Failed();
            }


            throw new Exception("Error while trying to get user from claims.");
        }
    }
}