using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.RemoveReview.Commands
{
    public sealed class RemoveReviewCommandHandler : IRequestHandler<RemoveReviewCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RemoveReviewCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RemoveReviewCommand request, CancellationToken cancellationToken)
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

                await _dbContext.SaveChangesAsync();
                return Result.Succeeded(true);
            }

            return Result.Succeeded();
        }
    }
}