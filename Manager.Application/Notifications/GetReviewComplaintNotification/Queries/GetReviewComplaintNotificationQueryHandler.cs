using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application.Notifications.GetReviewComplaintNotification.Queries
{
    public sealed class GetReviewComplaintNotificationQueryHandler : IRequestHandler<GetReviewComplaintNotificationQuery, Result>
    {
        private readonly IWebsiteDbContext _websiteDbContext;
        private readonly IManagerDbContext _managerDbContext;

        public GetReviewComplaintNotificationQueryHandler(IWebsiteDbContext websiteDbContext, IManagerDbContext managerDbContext)
        {
            _websiteDbContext = websiteDbContext;
            _managerDbContext = managerDbContext;
        }



        public async Task<Result> Handle(GetReviewComplaintNotificationQuery request, CancellationToken cancellationToken)
        {
            var review = await _websiteDbContext.Notifications
                .Where(x => x.NotificationGroupId == request.NotificationGroupId)
                .Select(x => new
                {
                    x.ReviewId,
                    x.ProductReview.Deleted,
                    ReviewWriter = new
                    {
                        x.ProductReview.UserId,
                        x.ProductReview.User.FirstName,
                        x.ProductReview.User.LastName,
                        x.ProductReview.User.Image,
                        x.ProductReview.User.Email,
                        reviewTitle = x.ProductReview.Title,
                        x.ProductReview.Date,
                        x.ProductReview.User.NoncompliantStrikes,
                        x.ProductReview.User.BlockNotificationSending,
                        Text = Utility.TextToHTML(x.ProductReview.Text)
                    }
                }).FirstAsync();

            var users = await _websiteDbContext.Notifications
                .OrderByDescending(x => x.CreationDate)
                .Where(x => x.NotificationGroupId == request.NotificationGroupId)
                .Select(x => new
                {
                    x.UserId,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Image,
                    x.User.Email,
                    Text = x.Text != null ? Utility.TextToHTML(x.Text): null,
                    Date = x.CreationDate,
                    x.User.NoncompliantStrikes,
                    x.User.BlockNotificationSending
                }).ToListAsync();

            // Get the employee notes from this notification
            var employeeNotes = await _managerDbContext.NotificationEmployeeNotes
                .OrderByDescending(x => x.CreationDate)
                .Where(x => x.NotificationGroupId == request.NotificationGroupId)
                .Select(x => new
                {
                    x.NotificationId,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Image,
                    x.User.Email,
                    Text = x.Note,
                    Date = x.CreationDate
                }).ToListAsync();

            return Result.Succeeded(new
            {
                review.ReviewId,
                ReviewDeleted = review.Deleted,
                Users = users,
                review.ReviewWriter,
                employeeNotes
            });
        }
    }
}