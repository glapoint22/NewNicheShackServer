using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;

namespace Website.Application.Lists.GetCollaboratorProducts.Queries
{
    public sealed class GetCollaboratorProductsQueryHandler : CollaboratorProductHandler, IRequestHandler<GetCollaboratorProductsQuery, Result>
    {

        public GetCollaboratorProductsQueryHandler(IWebsiteDbContext dbContext, IUserService userService) : base(dbContext, userService) { }

        public async Task<Result> Handle(GetCollaboratorProductsQuery request, CancellationToken cancellationToken)
        {
            List<CollaboratorProductDto> products = await GetProducts(request.ListId, request.Sort);

            return Result.Succeeded(products);
        }
    }
}