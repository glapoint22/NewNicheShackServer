using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.Common.Entities;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;

namespace Website.Application.Lists.GetListProducts.Queries
{
    public sealed class GetListProductsQueryHandler : IRequestHandler<GetListProductsQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        public GetListProductsQueryHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result> Handle(GetListProductsQuery request, CancellationToken cancellationToken)
        {
            string? userTrackingCode = null;
            User user = await _userService.GetUserFromClaimsAsync();

            var products = await _dbContext.ListProducts
                .SortBy(request.Sort)
                .Where(x => x.ListId == request.ListId)
                .Select(x => new ListProductDto
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
                })
                .ToListAsync();

            return Result.Succeeded(products);
        }
    }
}