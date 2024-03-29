﻿using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.ProductReviews.RateReview.Commands
{
    public sealed class RateReviewCommandHandler : IRequestHandler<RateReviewCommand, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public RateReviewCommandHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RateReviewCommand request, CancellationToken cancellationToken)
        {
            ProductReview review = (await _dbContext.ProductReviews.FindAsync(request.ReviewId))!;

            review.Rate(request.Likes, request.Dislikes);

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}