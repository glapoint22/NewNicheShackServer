using Shared.Common.Entities;
using Website.Application.Common.Classes;

namespace Website.Application.ProductReviews.Classes
{
    public static class Extensions
    {
        // -------------------------------------------------------------------------- Sort By ---------------------------------------------------------------------------------
        public static IOrderedQueryable<ProductReview> SortBy(this IQueryable<ProductReview> source, string? sortBy)
        {
            IOrderedQueryable<ProductReview> result;

            switch (sortBy)
            {
                case "low-to-high":
                    result = source
                        .OrderBy(x => x.Rating)
                        .ThenByDescending(x => x.Date);
                    break;

                case "newest-to-oldest":
                    result = source.OrderByDescending(x => x.Date);
                    break;

                case "oldest-to-newest":
                    result = source.OrderBy(x => x.Date);
                    break;

                case "most-helpful":
                    result = source
                        .OrderByDescending(x => x.Likes)
                        .ThenByDescending(x => x.Date);
                    break;

                default:
                    // High to low rating
                    result = source
                        .OrderByDescending(x => x.Rating)
                        .ThenByDescending(x => x.Date);
                    break;
            }

            return result;
        }




        // --------------------------------------------------------------------------- Where ---------------------------------------------------------------------------------
        public static IQueryable<ProductReview> Where(this IQueryable<ProductReview> source, string? filterBy)
        {
            switch (filterBy)
            {
                case "five-stars":
                    source = source.Where(x => x.Rating == 5);
                    break;
                case "four-stars":
                    source = source.Where(x => x.Rating == 4);
                    break;

                case "three-stars":
                    source = source.Where(x => x.Rating == 3);
                    break;


                case "two-stars":
                    source = source.Where(x => x.Rating == 2);
                    break;


                case "one-star":
                    source = source.Where(x => x.Rating == 1);
                    break;

                default:
                    source = source.Where(x => x.Rating > 0);
                    break;
            }

            return source;
        }



        // -------------------------------------------------------------------------- Select ---------------------------------------------------------------------------------
        public static IQueryable<ProductReviewDto> Select(this IQueryable<ProductReview> source)
        {
            return source.Select(x => new ProductReviewDto
            {
                Id = x.Id,
                Title = x.Title,
                ProductId = x.ProductId,
                Rating = x.Rating,
                UserName = x.User.FirstName + " " + x.User.LastName,
                ProfileImage = new Image
                {
                    Name = x.User.FirstName + " " + x.User.LastName,
                    Src = x.User.Image!
                },
                Date = x.Date.ToString(),
                IsVerified = x.Product.ProductOrders.Any(z => z.UserId == x.UserId && z.ProductId == x.ProductId),
                Text = x.Text,
                Likes = x.Likes,
                Dislikes = x.Dislikes
            });
        }
    }
}