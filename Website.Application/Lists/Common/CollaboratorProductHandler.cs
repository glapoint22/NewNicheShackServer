using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;
using Shared.Common.Classes;

namespace Website.Application.Lists.Common
{
    public abstract class CollaboratorProductHandler
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IUserService _userService;

        protected CollaboratorProductHandler(IWebsiteDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }



        // ----------------------------------------------------------------------- Add Product ------------------------------------------------------------------------
        public CollaboratorProduct AddProduct(string productId, int collaboratorId)
        {
            CollaboratorProduct collaboratorProduct = new(productId, collaboratorId);
            _dbContext.CollaboratorProducts.Add(collaboratorProduct);

            return collaboratorProduct;
        }




        // ----------------------------------------------------------------------- Get Products ------------------------------------------------------------------------
        public async Task<List<CollaboratorProductDto>> GetProducts(string listId, string? sort = null)
        {
            string? userTrackingCode = null;
            User user = await _userService.GetUserFromClaimsAsync();

            return await _dbContext.CollaboratorProducts
                .SortBy(sort)
                .Where(x => x.Collaborator.ListId == listId)
                .Select(x => new CollaboratorProductDto
                {
                    Id = x.Id,
                    Name = x.Product.Name,
                    Rating = x.Product.Rating,
                    TotalReviews = x.Product.TotalReviews,
                    MinPrice = x.Product.ProductPrices.MinPrice(),
                    MaxPrice = x.Product.ProductPrices.MaxPrice(),
                    DateAdded = x.DateAdded.ToString(),
                    Collaborator = new CollaboratorDto
                    {
                        Id = x.Collaborator.Id,
                        Name = x.Collaborator.User.FirstName,
                        ProfileImage = new Image
                        {
                            Name = x.Collaborator.User.FirstName,
                            Src = x.Collaborator.User.Image!
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
        }





        // ---------------------------------------------------------------------- Remove Product ----------------------------------------------------------------------
        public async Task<CollaboratorProduct> RemoveProduct(Guid collaboratorProductId)
        {
            CollaboratorProduct product = await _dbContext.CollaboratorProducts
                 .Where(x => x.Id == collaboratorProductId)
                 .Include(x => x.Collaborator)
                 .SingleAsync();

            _dbContext.CollaboratorProducts.Remove(product);

            return product;
        }
    }
}