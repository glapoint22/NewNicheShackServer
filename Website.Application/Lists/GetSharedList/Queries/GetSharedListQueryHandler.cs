using MediatR;
using Microsoft.EntityFrameworkCore;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Application.Lists.GetCollaboratorProducts.Queries;

namespace Website.Application.Lists.GetSharedList.Queries
{
    public sealed class GetSharedListQueryHandler : IRequestHandler<GetSharedListQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly ISender _mediator;

        public GetSharedListQueryHandler(IWebsiteDbContext dbContext, ISender mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Result> Handle(GetSharedListQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCollaboratorProductsQuery(request.ListId, request.Sort), cancellationToken);

            var listName = await _dbContext.Lists
                .Where(x => x.Id == request.ListId)
                .Select(x => x.Name)
                .SingleAsync(cancellationToken: cancellationToken);

            return Result.Succeeded(new
            {
                request.ListId,
                ListName = listName,
                Products = result.ObjContent
            });
        }
    }
}