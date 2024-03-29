﻿using MediatR;
using Shared.Common.Classes;

namespace Website.Application.ProductReviews.GetPositiveNegativeReviews.Queries
{
    public sealed record GetPositiveNegativeReviewsQuery(Guid ProductId) : IRequest<Result>;
}