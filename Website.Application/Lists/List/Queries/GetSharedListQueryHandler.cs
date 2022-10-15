using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.List.Common;

namespace Website.Application.Lists.List.Queries
{
    public class GetSharedListQueryHandler : IRequestHandler<GetSharedListQuery, SharedList>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetSharedListQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SharedList> Handle(GetSharedListQuery request, CancellationToken cancellationToken)
        {
            var sharedList = await _dbContext.Lists
                .Where(list => list.Id == request.ListId)
                .Select(list => new SharedList
                {
                    ListId = request.ListId,
                    ListName = list.Name,
                    Products = list.Collaborators
                        .Select(collaborator => collaborator.CollaboratorProducts.ToList())
                        .SelectMany(collaboratorProductList => collaboratorProductList
                            .Select(collaboratorProduct => new ListProductDto
                            {
                                Name = collaboratorProduct.Product.Name
                            })).ToList()
                }).SingleOrDefaultAsync();

            return sharedList!;
        }
    }
}