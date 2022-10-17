using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;
using Website.Application.Lists.SharedList.Classes;

namespace Website.Application.Lists.SharedList.Queries
{
    public class GetSharedListQueryHandler : IRequestHandler<GetSharedListQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetSharedListQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetSharedListQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.CollaboratorProducts
                .SortBy(request.Sort)
                .Where(x => x.Collaborator.ListId == request.ListId)
                .Select(x => new CollaboratorProductDto
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Rating = x.Product.Rating,
                    TotalReviews = x.Product.TotalReviews,
                    MinPrice = x.Product.ProductPrices.Min(z => z.Price),
                    MaxPrice = x.Product.ProductPrices.Count > 1 ? x.Product.ProductPrices.Max(z => z.Price) : 0,
                    Date = x.DateAdded.ToString(),
                    Collaborator = new CollaboratorDto
                    {
                        Id = x.Collaborator.Id,
                        Name = x.Collaborator.User.FirstName,
                        ProfileImage = new ImageDto
                        {
                            Name = x.Collaborator.User.FirstName,
                            Src = x.Collaborator.User.Image!
                        }
                    },
                    Hoplink = x.Product.Hoplink,
                    Image = new ImageDto
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
                })
                .ToListAsync();


            var listName = await _dbContext.Lists
                .Where(x => x.Id == request.ListId)
                .Select(x => x.Name)
                .SingleAsync();

            return Result.Succeeded(new SharedListDto
            {
                ListId = request.ListId,
                ListName = listName,
                Products = products
            });
        }
    }
}