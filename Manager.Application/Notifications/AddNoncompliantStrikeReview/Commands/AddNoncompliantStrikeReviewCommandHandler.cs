using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
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
                if (request.AddStrike) user.NoncompliantStrikes++;
                user.ProductReviews.First().Deleted = true;

                await _dbContext.SaveChangesAsync();
                return Result.Succeeded(true);
            }

            return Result.Succeeded();
        }
    }
}