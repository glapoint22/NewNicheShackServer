using MediatR;
using Microsoft.EntityFrameworkCore;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.DeleteAccount.Commands
{
    public sealed class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Result>
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

            if (user == null) throw new Exception("Error while trying to get user from claims.");


            if (await _userService.VerifyDeleteAccountTokenAsync(user, request.Token) && await _userService.CheckPasswordAsync(user, request.Password))
            {
                List<List> lists = await _dbContext.Collaborators
                .Where(x => x.UserId == user.Id && x.IsOwner)
                .Select(x => x.List)
                .ToListAsync();

                _dbContext.Lists.RemoveRange(lists);
                _dbContext.Users.Remove(user);

                user.AddDomainEvent(new UserDeletedEvent(user.Id));
                await _dbContext.SaveChangesAsync();

                return Result.Succeeded();
            }

            return Result.Failed();
        }
    }
}