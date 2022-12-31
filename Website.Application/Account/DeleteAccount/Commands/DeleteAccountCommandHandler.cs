using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
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

            if (!await _userService.CheckPasswordAsync(user, request.Password)) return Result.Failed("401");

            if (!await _userService.VerifyDeleteAccountTokenAsync(user, request.OneTimePassword)) return Result.Failed("409");


            // Get all the lists from this user
            List<List> lists = await _dbContext.Collaborators
            .Where(x => x.UserId == user.Id && x.IsOwner)
            .Select(x => x.List)
            .ToListAsync();

            // Notifications
            List<Notification> notifications = await _dbContext.Notifications
                .Where(x => x.UserId == user.Id)
                .Include(z => z.NotificationGroup)
                .ToListAsync();

            if (notifications.Count > 0)
            {
                _dbContext.Notifications.RemoveRange(notifications);

                List<NotificationGroup> groups = notifications.Select(x => x.NotificationGroup).ToList();
                _dbContext.NotificationGroups.RemoveRange(groups);
            }

            // Remove
            _dbContext.Lists.RemoveRange(lists);
            _dbContext.Users.Remove(user);

            user.AddDomainEvent(new UserDeletedEvent(user.FirstName, user.LastName, user.Email));
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}