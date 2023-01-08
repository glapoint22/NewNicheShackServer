using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.RemoveRestoreReview.Commands
{
    internal class RemoveRestoreReviewCommandHandler : IRequestHandler<RemoveRestoreReviewCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RemoveRestoreReviewCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RemoveRestoreReviewCommand request, CancellationToken cancellationToken)
        {
            User? user = await _dbContext.Users
                .Where(x => x.Id == request.UserId)
                .Include(x => x.ProductReviews
                    .Where(z => z.Id == request.ReviewId))
                .SingleOrDefaultAsync();



            if (user != null)
            {
                ProductReview productReview = user.ProductReviews.First();

                if (request.AddStrike)
                {
                    user.AddStrike();
                    DomainEventsInterceptor.AddDomainEvent(new UserReceivedNoncompliantStrikeReviewEvent(
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        productReview.Title,
                        productReview.Text));
                }
                productReview.RemoveRestore();
            }


            if (request.IsNew)
            {
                NotificationGroup? notificationGroup = await _dbContext.NotificationGroups
                    .Where(x => x.Id == request.NotificationGroupId)
                    .SingleOrDefaultAsync();

                notificationGroup?.ArchiveGroup();
            }


            await _dbContext.SaveChangesAsync();


            return Result.Succeeded();
        }
    }
}