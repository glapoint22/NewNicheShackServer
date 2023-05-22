using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Domain.Entities;

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
        public static IQueryable<ListProductDto> Select(this IQueryable<ListProduct> source, User? user)
        {
            string? userTrackingCode = null;
            if (user != null) userTrackingCode = user.TrackingCode;

            return source.Select(x => new ListProductDto
            {
                Id = x.ProductId,
                Name = x.Product.Name,
                Rating = x.Product.Rating,
                TotalReviews = x.Product.TotalReviews,
                MinPrice = x.Product.ProductPrices.MinPrice(),
                MaxPrice = x.Product.ProductPrices.MaxPrice(),
                DateAdded = x.DateAdded.ToString(),
                Disabled = x.Product.Disabled,
                Collaborator = new CollaboratorDto
                {
                    Id = x.UserId,
                    Name = user != null && user.Id == x.UserId ? "you" : x.User.FirstName,
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