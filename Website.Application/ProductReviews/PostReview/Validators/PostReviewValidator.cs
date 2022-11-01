using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.ProductReviews.PostReview.Commands;

namespace Website.Application.ProductReviews.PostReview.Validators
{
    public sealed class PostReviewValidator : AbstractValidator<PostReviewCommand>
    {
        public PostReviewValidator(IWebsiteDbContext dbContext)
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .MustAsync(async (productId, cancellation) =>
                {
                    return await dbContext.Products.AnyAsync(x => x.Id == productId);
                }).WithMessage("Product does not exists");

            RuleFor(x => x.Rating)
                .NotEmpty()
                .GreaterThan(0)
                .LessThanOrEqualTo(5);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Text)
                .NotEmpty();
        }
    }
}