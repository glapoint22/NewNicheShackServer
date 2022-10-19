using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Lists.CollaboratorProducts.Queries
{
    public class GetCollaboratorProductsQueryHandler : IRequestHandler<GetCollaboratorProductsQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetCollaboratorProductsQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetCollaboratorProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.CollaboratorProducts
                .SortBy(request.Sort)
                .Where(x => x.Collaborator.ListId == request.ListId)
                .Select(x => new
                {
                    x.Product.Id,
                    x.Product.Name,
                    x.Product.Rating,
                    x.Product.TotalReviews,
                    MinPrice = x.Product.ProductPrices.Min(z => z.Price),
                    MaxPrice = x.Product.ProductPrices.Count > 1 ? x.Product.ProductPrices.Max(z => z.Price) : 0,
                    DateAdded = x.DateAdded.ToString(),
                    Collaborator = new
                    {
                        x.Collaborator.Id,
                        Name = x.Collaborator.User.FirstName,
                        ProfileImage = new
                        {
                            Name = x.Collaborator.User.FirstName,
                            Src = x.Collaborator.User.Image
                        }
                    },
                    x.Product.Hoplink,
                    Image = new
                    {
                        x.Product.Media.Name,
                        Src = x.Product.Media.ImageSm
                    },
                    x.Product.UrlName,
                    x.Product.OneStar,
                    x.Product.TwoStars,
                    x.Product.ThreeStars,
                    x.Product.FourStars,
                    x.Product.FiveStars
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return Result.Succeeded(products);
        }
    }
}