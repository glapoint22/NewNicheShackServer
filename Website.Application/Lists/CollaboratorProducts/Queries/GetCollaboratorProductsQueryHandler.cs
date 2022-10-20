using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.Common;

namespace Website.Application.Lists.CollaboratorProducts.Queries
{
    public class GetCollaboratorProductsQueryHandler : CollaboratorProductHandler, IRequestHandler<GetCollaboratorProductsQuery, Result>
    {

        public GetCollaboratorProductsQueryHandler(IWebsiteDbContext dbContext) : base(dbContext) { }

        public async Task<Result> Handle(GetCollaboratorProductsQuery request, CancellationToken cancellationToken)
        {
            List<CollaboratorProductDto> products = await GetProducts(request.ListId, request.Sort);

            return Result.Succeeded(products);
        }
    }
}