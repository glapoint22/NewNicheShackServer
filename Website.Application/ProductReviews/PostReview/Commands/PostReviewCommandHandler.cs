﻿using MediatR;
using Shared.Common.Entities;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Events;

namespace Website.Application.ProductReviews.PostReview.Commands
{
    public sealed class PostReviewCommandHandler : IRequestHandler<PostReviewCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public PostReviewCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(PostReviewCommand request, CancellationToken cancellationToken)
        {
            // Get the product
            Product? product = await _dbContext.Products.FindAsync(request.ProductId);
            if (product == null) return Result.Failed("404");

            string userId = _userService.GetUserIdFromClaims();

            // Create the product review
            ProductReview productReview = ProductReview.Create(request.ProductId, userId, request.Title, request.Rating, request.Text);
            _dbContext.ProductReviews.Add(productReview);

            // Set the product rating
            product.SetRating(request.Rating);


            // Update the product and save the changes to the database
            _dbContext.Products.Update(product);


            product.AddDomainEvent(new PostedReviewEvent(userId, request.ProductId, request.Rating, request.Title, request.Text));
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}