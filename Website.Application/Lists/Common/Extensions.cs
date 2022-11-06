using Shared.Common.Classes;
using Shared.Common.Entities;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.Common
{
    public static class Extensions
    {
        // -------------------------------------------------------------------------- Sort By ---------------------------------------------------------------------------------
        public static IOrderedQueryable<ListProduct> SortBy(this IQueryable<ListProduct> source, string? sortBy)
        {
            IOrderedQueryable<ListProduct> result;


            switch (sortBy)
            {
                case "price-asc":
                    result = source.OrderBy(x => x.Product.ProductPrices.Min(z => z.Price));
                    break;
                case "price-desc":
                    result = source.OrderByDescending(x => x.Product.ProductPrices.Max(z => z.Price));
                    break;
                case "rating":
                    result = source.OrderByDescending(x => x.Product.Rating);
                    break;
                case "title":
                    result = source.OrderBy(x => x.Product.Name);
                    break;
                default:
                    result = source.OrderByDescending(x => x.DateAdded);
                    break;
            }

            return result;
        }





        // -------------------------------------------------------------------------- Select ---------------------------------------------------------------------------------
        public static IQueryable<ListProductDto> Select(this IQueryable<ListProduct> source, string? userTrackingCode)
        {
            return source.Select(x => new ListProductDto
            {
                ProductId = x.ProductId,
                Name = x.Product.Name,
                Rating = x.Product.Rating,
                TotalReviews = x.Product.TotalReviews,
                MinPrice = x.Product.ProductPrices.MinPrice(),
                MaxPrice = x.Product.ProductPrices.MaxPrice(),
                DateAdded = x.DateAdded.ToString(),
                Collaborator = new CollaboratorDto
                {
                    Id = x.UserId,
                    Name = x.User.FirstName,
                    ProfileImage = new Image
                    {
                        Name = x.User.FirstName,
                        Src = x.User.Image!
                    }
                },
                Hoplink = x.Product.GetHoplink(userTrackingCode),
                Image = new Image
                {
                    Name = x.Product.Media.Name,
                    Src = x.Product.Media.ImageSm!
                },
                UrlName = x.Product.UrlName,
                OneStar = x.Product.OneStar,
                TwoStars = x.Product.TwoStars,
                ThreeStars = x.Product.ThreeStars,
                FourStars = x.Product.FourStars,
                FiveStars = x.Product.FiveStars
            });
        }
    }
}