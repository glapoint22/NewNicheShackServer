using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using User = Website.Domain.Entities.User;

namespace Manager.Application.Notifications.ReplaceUserName.Commands
{
    public sealed class ReplaceUserNameCommandHandler : IRequestHandler<ReplaceUserNameCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public ReplaceUserNameCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(ReplaceUserNameCommand request, CancellationToken cancellationToken)
        {
            User? user = await _dbContext.Users
                .Where(x => x.Id == request.UserId)
                .SingleOrDefaultAsync();

            if (user != null && user.FirstName + " " + user.LastName == request.UserName)
            {
                user.AddStrike();
                user.ChangeName("NicheShack", "User");

                await _dbContext.SaveChangesAsync();
                DomainEventsInterceptor.AddDomainEvent(new UserReceivedNoncompliantStrikeUserNameEvent(user.FirstName, user.LastName, user.Email));

                return Result.Succeeded(true);
            }

            return Result.Succeeded();
        }
    }
}