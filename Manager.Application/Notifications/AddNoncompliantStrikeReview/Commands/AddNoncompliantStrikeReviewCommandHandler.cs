using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Interceptors;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.AddNoncompliantStrikeReview.Commands
{
    public sealed class AddNoncompliantStrikeReviewCommandHandler : IRequestHandler<AddNoncompliantStrikeReviewCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public AddNoncompliantStrikeReviewCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddNoncompliantStrikeReviewCommand request, CancellationToken cancellationToken)
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
                    user.NoncompliantStrikes++;
                    DomainEventsInterceptor.AddDomainEvent(new UserReceivedNoncompliantStrikeReviewEvent(
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        productReview.Title,
                        productReview.Text));
                }

                productReview.Deleted = true;

                await _dbContext.SaveChangesAsync();
                return Result.Succeeded(true);
            }

            return Result.Succeeded();
        }
    }
}