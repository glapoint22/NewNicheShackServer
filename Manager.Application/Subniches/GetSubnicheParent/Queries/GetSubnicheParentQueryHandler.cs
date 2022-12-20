using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.GetSubnicheParent.Queries
{
    public sealed class GetSubnicheParentQueryHandler : IRequestHandler<GetSubnicheParentQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetSubnicheParentQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetSubnicheParentQuery request, CancellationToken cancellationToken)
        {
            var niche = await _dbContext.Subniches
                .Where(x => x.Id == request.ChildId)
                .Select(x => new
                {
                    Id = x.NicheId,
                    x.Niche.Name
                }).SingleAsync();

            return Result.Succeeded(niche);
        }
    }
}