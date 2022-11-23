using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.AddNoncompliantStrikeUserImage.Commands
{
    public sealed class AddNoncompliantStrikeUserImageCommandHandler : IRequestHandler<AddNoncompliantStrikeUserImageCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public AddNoncompliantStrikeUserImageCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddNoncompliantStrikeUserImageCommand request, CancellationToken cancellationToken)
        {
            User? user = await _dbContext.Users
                .Where(x => x.Id == request.UserId)
                .SingleOrDefaultAsync();

            if (user != null && user.Image == request.UserImage)
            {
                user.NoncompliantStrikes++;
                user.Image = null;

                //user.AddDomainEvent(new UserReceivedNoncompliantStrikeUserImageEvent(user.Id));

                await _dbContext.SaveChangesAsync();
                return Result.Succeeded(true);
            }

            return Result.Succeeded();
        }
    }
}