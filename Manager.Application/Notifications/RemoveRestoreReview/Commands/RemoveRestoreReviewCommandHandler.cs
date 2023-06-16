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
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Media)
                .SingleOrDefaultAsync();



            if (user != null)
            {
                ProductReview productReview = user.ProductReviews.First();

                string stars = await GetStarsImage(productReview.Rating);

                if (request.AddStrike)
                {
                    user.AddStrike();
                    DomainEventsInterceptor.AddDomainEvent(new UserReceivedNoncompliantStrikeReviewEvent(
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        productReview.Title,
                        productReview.Text,
                        productReview.ProductId,
                        productReview.Product.Name,
                        productReview.Product.UrlName,
                        productReview.Product.Media.ImageSm!,
                        stars));
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



        private async Task<string> GetStarsImage(double rating)
        {
            string imageName = string.Empty;

            switch (rating)
            {
                case 1:
                    imageName = "One Star";
                    break;

                case 2:
                    imageName = "Two Stars";
                    break;

                case 3:
                    imageName = "Three Stars";
                    break;

                case 4:
                    imageName = "Four Stars";
                    break;

                case 5:
                    imageName = "Five Stars";
                    break;
            }


            return await _dbContext.Media
                .Where(x => x.Name == imageName)
                .Select(x => x.ImageAnySize!)
                .SingleAsync();
        }
    }
}