using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using User = Website.Domain.Entities.User;

namespace Manager.Application.Notifications.AddNoncompliantStrikeUserName.Commands
{
    public sealed class AddNoncompliantStrikeUserNameCommandHandler : IRequestHandler<AddNoncompliantStrikeUserNameCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public AddNoncompliantStrikeUserNameCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddNoncompliantStrikeUserNameCommand request, CancellationToken cancellationToken)
        {
            User? user = await _dbContext.Users
                .Where(x => x.Id == request.UserId)
                .SingleOrDefaultAsync();

            if (user != null && user.FirstName + " " + user.LastName == request.UserName)
            {
                DomainEventsInterceptor.AddDomainEvent(new UserReceivedNoncompliantStrikeUserNameEvent(user.FirstName, user.LastName, user.Email));

                user.NoncompliantStrikes++;
                user.ChangeName("NicheShack", "User");

                await _dbContext.SaveChangesAsync();
                return Result.Succeeded(true);
            }

            return Result.Succeeded();
        }
    }
}