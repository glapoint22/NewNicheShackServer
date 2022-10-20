using Website.Domain.Entities;

namespace Website.Application.Common.Classes
{
    public static class Extensions
    {
        public static IOrderedQueryable<CollaboratorProduct> SortBy(this IQueryable<CollaboratorProduct> source, string? sortBy)
        {
            IOrderedQueryable<CollaboratorProduct> orderResult;


            switch (sortBy)
            {
                case "price-asc":
                    orderResult = source.OrderBy(x => x.Product.ProductPrices.Min(z => z.Price));
                    break;
                case "price-desc":
                    orderResult = source.OrderByDescending(x => x.Product.ProductPrices.Max(z => z.Price));
                    break;
                case "rating":
                    orderResult = source.OrderByDescending(x => x.Product.Rating);
                    break;
                case "title":
                    orderResult = source.OrderBy(x => x.Product.Name);
                    break;
                default:
                    orderResult = source.OrderByDescending(x => x.DateAdded);
                    break;
            }

            return orderResult;
        }
    }
}